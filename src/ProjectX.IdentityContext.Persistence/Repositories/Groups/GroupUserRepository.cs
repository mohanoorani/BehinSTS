using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using ProjectX.IdentityContext.Domain.Exceptions;

namespace ProjectX.IdentityContext.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext, TUser, TKey>
    {
        public async Task AddUser(string groupName, string username)
        {
            var group = await dbContext.Groups.FirstOrDefaultAsync(i => i.Name.ToLower() == groupName.ToLower());
            if (group == null) throw new EntityNotFoundException();

            var user = await userManager.FindByNameAsync(username);
            if (user == null) throw new EntityNotFoundException();

            group.Users.Add(new GroupUser {UserId = user.Id.ToString()});
            group.Updated = DateTime.Now;

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

        public async Task RemoveUser(string groupName, string username)
        {
            var group = dbContext.Groups
                .Include(i => i.Users)
                .First(i => i.Name.ToLower() == groupName.ToLower());

            group.Users.RemoveAll(i => i.User.UserName.ToLower() == username.ToLower());
            group.Updated = DateTime.Now;
            
            await dbContext.SaveChangesAsync();
        }
    }
}