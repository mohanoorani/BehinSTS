using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectX.Application.Services.Interfaces;

namespace Skoruba.IdentityServer4.Admin.UserEvent.Worker
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serviceProvider = Helper.GetServices();
            var userEventService = serviceProvider.GetRequiredService<IUserEventService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var userEvents = await userEventService.GetAll();

                userEvents.ForEach(Helper.SendToQueue);

                //Helper.RecieveMessages();
                //await userEventService.RemoveAll();

                await Task.Delay(5000000, stoppingToken);
            }
        }
    }
}