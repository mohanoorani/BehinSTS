using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Audits;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Audits;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace ProjectX.IdentityContext.Persistence.Repositories.Audits
{
    public class AuditRepository<TDbContext> : IAuditRepository
        where TDbContext : DbContext, IAdminLogDbContext
    {
        private readonly TDbContext dbContext;

        public AuditRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AutoSaveChanges { get; set; } = true;

        public async Task Add(Audit log)
        {
            await dbContext.Audits.AddAsync(log);
            //return dbContext.SaveChangesAsync();
        }

        public Task<List<Audit>> GetAll()
        {
            return dbContext.Audits.OrderBy(i => i.EventTime).ToListAsync();
        }

        public async Task RemoveAll()
        {
            var audits = await GetAll();
            dbContext.Audits.RemoveRange(audits);
            //dbContext.SaveChanges();
        }
    }
}