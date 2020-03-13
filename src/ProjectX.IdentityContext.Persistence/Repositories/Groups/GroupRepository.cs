using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace ProjectX.IdentityContext.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext> : IGroupRepository
        where TDbContext : DbContext, IAdminLogDbContext
    {
        private readonly TDbContext dbContext;

        public GroupRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Add(Group group)
        {
            group.Created = DateTime.Now;

            await dbContext.Groups.AddAsync(group);
        }

        public Task<List<Group>> GetAll()
        {
            return dbContext.Groups
                .Include(i => i.Creator)
                .Include(i => i.Updater)
                .OrderBy(i => i.Name).ToListAsync();
        }

        public Task<Group> GetByName(string name)
        {
            return dbContext.Groups
                .Include(i => i.ChildGroups)
                .Include(i => i.ParentGroups)
                .Include(i => i.Users).ThenInclude(i => i.User)
                .FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());
        }

        public async Task Update(
            string name,
            string newName,
            string description,
            string updaterId)
        {
            var group = await GetByName(name).ConfigureAwait(false);

            group.Name = newName;
            group.Description = description;
            group.UpdaterId = updaterId;
            group.Updated = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        public Task Remove(Group group)
        {
            dbContext.Groups.Remove(group);
            return dbContext.SaveChangesAsync();
        }
    }
}