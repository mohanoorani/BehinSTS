using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext>
    {
        public async Task AddChildGroup(GroupChildGroup childGroup, string updaterId)
        {
            var group = dbContext.Groups.First(i => i.Id == childGroup.ParentGroupId);

            group.ChildGroups.Add(childGroup);
            group.Updated = DateTime.Now;
            group.UpdaterId = updaterId;

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

        public async Task RemoveChildGroup(GroupChildGroup childGroup, string updaterId)
        {
            var group = await dbContext.Groups
                .Include(i => i.ChildGroups)
                .FirstAsync(i => i.Id == childGroup.ParentGroupId);

            group.ChildGroups.RemoveAll(i => i.ParentGroupId == childGroup.ParentGroupId && 
                                             i.ChildGroupId == childGroup.ChildGroupId);
            group.Updated = DateTime.Now;
            group.UpdaterId = updaterId;

            await dbContext.SaveChangesAsync();

        }
    }
}