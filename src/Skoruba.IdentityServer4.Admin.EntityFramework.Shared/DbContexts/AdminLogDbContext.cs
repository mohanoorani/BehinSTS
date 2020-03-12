using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Shared.DbContexts
{
    public class AdminLogDbContext : DbContext, IAdminLogDbContext
    {
        public AdminLogDbContext(DbContextOptions<AdminLogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Audit> Audits { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupChildGroup> GroupChildGroups { get; set; }

        public DbSet<GroupUser> GroupUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("citext");

            base.OnModelCreating(builder);

            ConfigureLogContext(builder);
        }

        private void ConfigureLogContext(ModelBuilder builder)
        {
            builder.Entity<Log>(log =>
            {
                log.ToTable(TableConsts.Logging);
                log.HasKey(x => x.Id);
                log.Property(x => x.Level).HasMaxLength(128);
            });

            builder.Entity<IdentityUser>(b => { b.ToTable("Users"); });

            builder.Entity<Group>(b =>
            {
                b.Property(x => x.Name).HasColumnType("citext");
                b.HasIndex(x => x.Name).IsUnique();

                b.HasMany(i => i.ChildGroups).WithOne(i => i.ParentGroup).HasForeignKey("ParentGroupId");
                b.HasMany(i => i.ParentGroups).WithOne(i => i.ChildGroup).HasForeignKey(i => i.ChildGroupId);
                
                b.HasOne(i => i.Creator).WithMany().HasForeignKey(i => i.CreatorId);
                b.HasOne(i => i.Updater).WithMany().HasForeignKey(i => i.UpdaterId);
            });

            builder.Entity<GroupChildGroup>(b =>
            {
                b.Property<Guid>("ParentGroupId");
                b.HasKey("ParentGroupId", "ChildGroupId");
            });

            builder.Entity<GroupUser>(b =>
            {
                b.Property<Guid>("GroupId");
                b.HasKey("GroupId", "UserId");

                //b.HasOne(i => i.User).WithMany();
            });

            //builder.Entity<Audit>(b =>
            //{
            //    b.HasOne(i => i.User).WithMany();
            //});
        }
    }

    public class DesignTimeAdminLogDbContextFactory : DesignTimeDbContextFactory<AdminLogDbContext>
    {
    }
}