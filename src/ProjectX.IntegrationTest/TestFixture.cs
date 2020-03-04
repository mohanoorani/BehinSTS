using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;

namespace ProjectX.IntegrationTest
{
    public class TestFixture : IDisposable
    {
        private const string AppSettingsFileName = "appsettings.json";

        private readonly TestServer testServer;

        public TestFixture()
        {
            var appSettingsFilePath = Path.Combine(AppContext.BaseDirectory, AppSettingsFileName);
            var appSettingsContent = File.ReadAllText(appSettingsFilePath);

            appSettingsContent = appSettingsContent.Replace("${dbName}", $"IdentityTest{DateTime.Now.Ticks}");
            File.WriteAllText(appSettingsFilePath, appSettingsContent);

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(AppSettingsFileName)
                .Build();

            var builder = new WebHostBuilder()
                .UseEnvironment("Production")
                .UseConfiguration(configurationRoot)
                .UseStartup<StartupProjectX>();

            testServer = new TestServer(builder);

            DbMigrationHelper.EnsureSeedData(testServer.Host.Services).GetAwaiter().GetResult();

            Client = testServer.CreateClient();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            var appSettingsFilePath = Path.Combine(AppContext.BaseDirectory, AppSettingsFileName);
            var dbContext = testServer.Host.Services.GetService<AdminLogDbContext>();

            dbContext.Database.EnsureDeleted();
            File.Delete(appSettingsFilePath);

            var permanentPath = Path.Combine(AppContext.BaseDirectory, "Permanent");
            var tempPath = Path.Combine(AppContext.BaseDirectory, "Temp");

            if (Directory.Exists(permanentPath))
            {
                foreach (var file in Directory.GetFiles(permanentPath))
                    File.Delete(file);

                Directory.Delete(permanentPath);
            }

            if (Directory.Exists(tempPath))
            {
                foreach (var file in Directory.GetFiles(tempPath))
                    File.Delete(file);

                Directory.Delete(tempPath);
            }

            Client.Dispose();
            testServer.Dispose();
        }
    }
}