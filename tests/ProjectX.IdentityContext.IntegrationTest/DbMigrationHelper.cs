using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Shared.Entities.Identity;

namespace ProjectX.IdentityContext.IntegrationTest
{
    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var identityServerConfigurationDbContext =
                serviceProvider.GetRequiredService<IdentityServerConfigurationDbContext>();
            await identityServerConfigurationDbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);

            var identityServerPersistedGrantDbContext =
                serviceProvider.GetRequiredService<IdentityServerPersistedGrantDbContext>();
            await identityServerPersistedGrantDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminIdentityDbContext = serviceProvider.GetRequiredService<AdminIdentityDbContext>();
            await adminIdentityDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminAuditLogDbContext = serviceProvider.GetRequiredService<AdminAuditLogDbContext>();
            await adminAuditLogDbContext.Database.MigrateAsync().ConfigureAwait(false);

            var adminLogDbContext = serviceProvider.GetRequiredService<AdminLogDbContext>();
            await adminLogDbContext.Database.MigrateAsync().ConfigureAwait(false);

            adminLogDbContext.Database.OpenConnection();
            ((NpgsqlConnection) adminLogDbContext.Database.GetDbConnection()).ReloadTypes();
            adminLogDbContext.Database.CloseConnection();

            SeedUser(adminIdentityDbContext);
            SeedGroup(adminLogDbContext);
        }

        private static void SeedUser(AdminIdentityDbContext adminIdentityDbContext)
        {
            var adminUser = new UserIdentity
            {
                Id = DbMigrationConstants.AdminUserId,
                UserName = DbMigrationConstants.AdminUsername,
                NormalizedUserName = DbMigrationConstants.AdminUsername.ToUpper(),
                Email = "AdminUser@AdminUser.com",
                NormalizedEmail = "ADMINUSER@ADMINUSER.COM",
                PhoneNumber = "0000",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true
            };

            var activeUser = new UserIdentity
            {
                Id = DbMigrationConstants.ActiveUserId,
                UserName = DbMigrationConstants.ActiveUsername,
                NormalizedUserName = DbMigrationConstants.ActiveUsername.ToUpper(),
                Email = "ActiveUser@ActiveUser.com",
                NormalizedEmail = "ACTIVEUSER@ACTIVEUSER.COM",
                PhoneNumber = "0000",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true
            };

            adminIdentityDbContext.Users.Add(activeUser);
            adminIdentityDbContext.Users.Add(adminUser);

            adminIdentityDbContext.SaveChanges();
        }

        private static void SeedGroup(AdminLogDbContext adminLogDbContext)
        {
            var group = new Group
                {Name = DbMigrationConstants.Group, Description = "Description", Created = DateTime.Now};
            var childGroup = new Group
                {Name = DbMigrationConstants.ChildGroup, Description = "Description", Created = DateTime.Now};

            adminLogDbContext.Groups.Add(group);
            adminLogDbContext.Groups.Add(childGroup);

            adminLogDbContext.SaveChanges();
        }
    }
}