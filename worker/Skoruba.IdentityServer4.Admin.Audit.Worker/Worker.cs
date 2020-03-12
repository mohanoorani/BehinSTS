using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectX.IdentityContext.Application.Services.Interfaces.Audits;

namespace Skoruba.IdentityServer4.Admin.Audit.Worker
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serviceProvider = Helper.GetServices();
            var auditService = serviceProvider.GetRequiredService<IAuditService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var audits = await auditService.GetAll();

                audits.ForEach(Helper.SendToQueue);

                //Helper.ReceiveMessages();
                //await auditService.RemoveAll();

                await Task.Delay(5000000, stoppingToken);
            }
        }
    }
}