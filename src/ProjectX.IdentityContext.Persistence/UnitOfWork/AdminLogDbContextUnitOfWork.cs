using Microsoft.EntityFrameworkCore;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace ProjectX.IdentityContext.Persistence.UnitOfWork
{
    public class AdminLogDbContextUnitOfWork<TDbContext> : UnitOfWork<TDbContext>
        where TDbContext : DbContext, IAdminLogDbContext
    {
        public AdminLogDbContextUnitOfWork(TDbContext context) : base(context)
        {
        }
    }
}
