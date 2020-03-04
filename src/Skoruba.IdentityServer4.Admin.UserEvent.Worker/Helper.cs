using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;

namespace Skoruba.IdentityServer4.Admin.UserEvent.Worker
{
    public static class Helper
    {
        public static IServiceProvider GetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AdminLogDbContext>(
                options => options.UseNpgsql(
                    "Username=postgres;Password=Abcd123$%^;Host=192.168.175.121;Database=IdentityServer4Admin2;"));

            services.AddAdminServices<
                IdentityServerConfigurationDbContext,
                IdentityServerPersistedGrantDbContext,
                AdminLogDbContext>();

            services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext,
                UserDto<string>, string, RoleDto<string>, string, string, string,
                UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
                UsersDto<UserDto<string>, string>, RolesDto<RoleDto<string>, string>,
                UserRolesDto<RoleDto<string>, string, string>,
                UserClaimsDto<string>, UserProviderDto<string>, UserProvidersDto<string>,
                UserChangePasswordDto<string>,
                RoleClaimsDto<string>, UserClaimDto<string>, RoleClaimDto<string>>();

            return services.BuildServiceProvider();
        }

        public static void SendToQueue(ProjectX.Domain.Entities.UserEvent userEvent)
        {
            try
            {
                const string rabbitMqAddress = "rabbitmq://192.168.175.121";
                const string rabbitMqQueue = "STS_UserEvents";
                var rabbitMqRootUri = new Uri(rabbitMqAddress);

                var rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
                {
                    rabbit.Host(rabbitMqRootUri, settings =>
                    {
                        settings.Password("123");
                        settings.Username("reza");
                    });
                });

                var rabbitUri = new Uri($"{rabbitMqAddress}/vhost/exchange_name?bind=true&queue={rabbitMqQueue}");
                var sendEndpointTask = rabbitBusControl.GetSendEndpoint(rabbitUri);
                var sendEndpoint = sendEndpointTask.Result;

                sendEndpoint.Send<ProjectX.Domain.Entities.UserEvent>(new
                {
                    userEvent.Id,
                    userEvent.EventValues,
                    userEvent.EventName,
                    userEvent.CreationDate
                });
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public static void RecieveMessages()
        {
            try
            {
                var rabbitBusControl = Bus.Factory.CreateUsingRabbitMq(rabbit =>
                {
                    rabbit.Host("rabbitmq://192.168.175.121", settings =>
                    {
                        settings.Password("123");
                        settings.Username("reza");
                    });

                    rabbit.ReceiveEndpoint("STS_UserEvents",
                        conf =>
                        {
                            conf.Batch<ProjectX.Domain.Entities.UserEvent>(b =>
                            {
                                b.MessageLimit = 100;
                                b.TimeLimit = TimeSpan.FromSeconds(1);
                                b.Consumer(() => new MyEventConsumer());
                            });
                        });
                });

                rabbitBusControl.StartAsync();

                rabbitBusControl.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class MyEventConsumer : IConsumer<Batch<ProjectX.Domain.Entities.UserEvent>>
        {
            public Task Consume(ConsumeContext<Batch<ProjectX.Domain.Entities.UserEvent>> context)
            {
                var builder = new StringBuilder();

                foreach (var userEvent in context.Message)
                    builder.Append(userEvent.Message.EventName);

                return Task.CompletedTask;
            }
        }
    }
}