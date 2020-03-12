using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Skoruba.IdentityServer4.Admin.Audit.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
        }
    }
}