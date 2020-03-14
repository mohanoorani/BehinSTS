using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext>
    {
        public async Task AddUser(GroupUser groupUser, string updaterId)
        {
            var group = await dbContext.Groups.FirstAsync(i => i.Id == groupUser.GroupId);

            group.Users.Add(groupUser);
            group.Updated = DateTime.Now;
            group.UpdaterId = updaterId;

            await dbContext.SaveChangesAsync();
        }

        public Task<List<GroupUser>> GetAllUsers(string groupName)
        {
            return dbContext.GroupUsers
                .Include(i => i.Group)
                .Include(i => i.User)
                .Where(i => i.Group.Name.ToLower() == groupName.ToLower())
                .ToListAsync();
        }

        public async Task RemoveUser(GroupUser groupUser, string updaterId)
        {
            var group = await dbContext.Groups
                .Include(i => i.Users)
                .FirstAsync(i => i.Id == groupUser.GroupId);

            group.Users.Remove(groupUser);
            group.Updated = DateTime.Now;
            group.UpdaterId = updaterId;

            await dbContext.SaveChangesAsync();
        }
    }
}