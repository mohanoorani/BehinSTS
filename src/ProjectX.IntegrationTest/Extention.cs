using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Skoruba.AuditLogging.EntityFramework.DbContexts;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.Api.Configuration.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.Extensions;

namespace ProjectX.IntegrationTest
{
    public static class Extention
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string url, object body)
        {
            return client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string url, object body)
        {
            return client.PutAsync(url,
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var contentString = await content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public static void AddDbContextsForProjectXTest<TIdentityDbContext, TConfigurationDbContext, TPersistedGrantDbContext,
            TLogDbContext, TAuditLoggingDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TIdentityDbContext : DbContext
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TConfigurationDbContext : DbContext, IAdminConfigurationDbContext
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TAuditLoggingDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
        {
            var randomId = "22";
            const string dbName = "IdentityServer4Admin2";

            var identityConnectionString = configuration.GetConnectionString(ConfigurationConsts.IdentityDbConnectionStringKey)
                .Replace(dbName, $"{dbName}-{randomId}");

            var configurationConnectionString = configuration.GetConnectionString(ConfigurationConsts.ConfigurationDbConnectionStringKey)
                .Replace(dbName, $"{dbName}-{randomId}");
            var persistedGrantsConnectionString = configuration.GetConnectionString(ConfigurationConsts.PersistedGrantDbConnectionStringKey)
                .Replace(dbName, $"{dbName}-{randomId}");

            var errorLoggingConnectionString = configuration.GetConnectionString(ConfigurationConsts.AdminLogDbConnectionStringKey)
                .Replace(dbName, $"{dbName}-{randomId}");

            var auditLoggingConnectionString = configuration.GetConnectionString(ConfigurationConsts.AdminAuditLogDbConnectionStringKey)
                .Replace(dbName, $"{dbName}-{randomId}");

            services.RegisterNpgSqlDbContexts<TIdentityDbContext, TConfigurationDbContext, TPersistedGrantDbContext,
                TLogDbContext, TAuditLoggingDbContext>(identityConnectionString, configurationConnectionString,
                persistedGrantsConnectionString, errorLoggingConnectionString, auditLoggingConnectionString);
        }
    }
}
