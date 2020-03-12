using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext, TUser, TKey>
    {
        public async Task AddChildGroup(Guid parentGroupId, Guid childGroupId)
        {
            var group = dbContext.Groups.First(i => i.Id == parentGroupId);

            group.ChildGroups.Add(new GroupChildGroup{ChildGroupId = childGroupId});
            group.Updated = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }

        public Task<List<GroupChildGroup>> GetAllChildGroups(Guid id)
        {
            return dbContext.GroupChildGroups
                .Include(i => i.ChildGroup)
                .Include(i => i.ParentGroup)
                .Where(i => i.ParentGroup.Id == id)
                .ToListAsync();
        }

        public async Task RemoveChildGroup(Guid id, Guid childGroupId)
        {
            var group = dbContext.Groups.Include(i => i.ChildGroups).First(i => i.Id == id);

            group.ChildGroups.RemoveAll(i => i.ChildGroupId == childGroupId);
            group.Updated = DateTime.Now;

            await dbContext.SaveChangesAsync();

        }
    }
}