using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectX.Domain.Entities.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;

namespace ProjectX.IntegrationTest
{
    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var identityServerConfigurationDbContext = serviceProvider.GetRequiredService<IdentityServerConfigurationDbContext>();
            await identityServerConfigurationDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var identityServerPersistedGrantDbContext = serviceProvider.GetRequiredService<IdentityServerPersistedGrantDbContext>();
            await identityServerPersistedGrantDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminIdentityDbContext = serviceProvider.GetRequiredService<AdminIdentityDbContext>();
            await adminIdentityDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminAuditLogDbContext = serviceProvider.GetRequiredService<AdminAuditLogDbContext>();
            await adminAuditLogDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminLogDbContext = serviceProvider.GetRequiredService<AdminLogDbContext>();
            await adminLogDbContext.Database.MigrateAsync().ConfigureAwait(false);

            await SeedGroup(adminLogDbContext).ConfigureAwait(false);
        }

        private static async Task SeedGroup(AdminLogDbContext adminLogDbContext)
        {
            var group = new Group{Name = DbMigrationConstants.Group, Description = "Description" };
            
            var childGroup = new Group{Name = DbMigrationConstants.ChildGroup, Description = "Description" };

            adminLogDbContext.Groups.Add(group);
            adminLogDbContext.Groups.Add(childGroup);

            await adminLogDbContext.SaveChangesAsync();
        }
    }
}
