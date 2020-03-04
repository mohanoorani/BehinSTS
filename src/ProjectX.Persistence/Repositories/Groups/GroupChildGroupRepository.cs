using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.Domain.Entities.Groups;

namespace ProjectX.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext, TUser, TKey>
    {
        public async Task AddChildGroup(Guid parentGroupId, Guid childGroupId)
        {
            var group = dbContext.Groups.First(i => i.Id == parentGroupId);

            group.ChildGroups.Add(new GroupChildGroup{ChildGroupId = childGroupId});

            await dbContext.SaveChangesAsync();
        }

        public Task<List<GroupChildGroup>> GetAllChildGroups(Guid parentGroupId)
        {
            return dbContext.GroupChildGroups
                .Include(i => i.ChildGroup)
                .Include(i => i.ParentGroup)
                .Where(i => i.ParentGroup.Id == parentGroupId)
                .ToListAsync();
        }

        public async Task RemoveChildGroup(Guid parentGroupId, Guid childGroupId)
        {
            var childGroup = dbContext.GroupChildGroups
                .FirstOrDefault(i => i.ChildGroupId == childGroupId && i.ParentGroup.Id == parentGroupId);

            if(childGroup == null) throw new Exception();

            dbContext.GroupChildGroups.Remove(childGroup);

            await dbContext.SaveChangesAsync();

        }
    }
}