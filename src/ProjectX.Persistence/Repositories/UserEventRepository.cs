using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Repositories.Interfaces;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace ProjectX.Persistence.Repositories
{
    public class UserEventRepository<TDbContext> : IUserEventRepository
        where TDbContext : DbContext, IAdminLogDbContext
    {
        private readonly TDbContext dbContext;

        public UserEventRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AutoSaveChanges { get; set; } = true;

        public Task<int> Add(UserEvent log)
        {
            dbContext.UserEvents.Add(log);
            return dbContext.SaveChangesAsync();
        }

        public Task<List<UserEvent>> GetAll()
        {
            return dbContext.UserEvents.OrderBy(i => i.CreationDate).ToListAsync();
        }

        public async Task RemoveAll()
        {
            var userEvents = await GetAll();
            dbContext.UserEvents.RemoveRange(userEvents);
            dbContext.SaveChanges();
        }
    }
}