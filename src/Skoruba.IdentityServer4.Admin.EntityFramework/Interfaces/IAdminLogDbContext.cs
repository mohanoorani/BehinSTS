using Microsoft.EntityFrameworkCore;
using ProjectX.Domain.Entities;
using ProjectX.Domain.Entities.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces
{
    public interface IAdminLogDbContext
    {
        DbSet<Log> Logs { get; set; }

        DbSet<UserEvent> UserEvents { get; set; }

        DbSet<Group> Groups { get; set; }

        DbSet<GroupChildGroup> GroupChildGroups { get; set; }

        DbSet<GroupUser> GroupUsers { get; set; }
    }
}
