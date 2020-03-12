using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;

namespace Skoruba.IdentityServer4.Admin.Audit.Worker
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

        public static void SendToQueue(ProjectX.IdentityContext.Domain.Entities.Audits.Audit audit)
        {
            try
            {
                const string rabbitMqAddress = "rabbitmq://192.168.175.121";
                const string rabbitMqQueue = "STS_Audits";
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

                sendEndpoint.Send<ProjectX.IdentityContext.Domain.Entities.Audits.Audit>(new
                {
                    audit.Id,
                    audit.AggregateId,
                    audit.Data,
                    audit.EventName,
                    audit.EventTime,
                    audit.Ip,
                    audit.UserId,
                    audit.UserAgent
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ReceiveMessages()
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

                    rabbit.ReceiveEndpoint("STS_Audits",
                        conf =>
                        {
                            conf.Batch<ProjectX.IdentityContext.Domain.Entities.Audits.Audit>(b =>
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

        public class MyEventConsumer : IConsumer<Batch<ProjectX.IdentityContext.Domain.Entities.Audits.Audit>>
        {
            public Task Consume(ConsumeContext<Batch<ProjectX.IdentityContext.Domain.Entities.Audits.Audit>> context)
            {
                var builder = new StringBuilder();

                foreach (var audit in context.Message)
                    builder.Append(audit.Message.EventName);

                return Task.CompletedTask;
            }
        }
    }
}