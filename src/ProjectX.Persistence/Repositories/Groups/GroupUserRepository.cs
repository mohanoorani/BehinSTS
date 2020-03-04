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
        public async Task AddUser(string groupName, string username)
        {
            var group = await dbContext.Groups.FirstOrDefaultAsync(i => i.Name.ToLower() == groupName.ToLower());
            if (group == null) throw new Exception();

            var user = await userManager.FindByNameAsync(username);
            if (user == null) throw new Exception();

            group.Users.Add(new GroupUser {UserId = user.Id.ToString()});

            await dbContext.SaveChangesAsync();
        }

        public Task<List<GroupUser>> GetAllUsers(string groupName)
        {
            try
            {
                var tt = dbContext.GroupUsers
                    .Include(i => i.Group)
                    .Include(i => i.User)
                    .Where(i => i.Group.Name.ToLower() == groupName.ToLower())
                    .ToList();


                return dbContext.GroupUsers
                    .Include(i => i.Group)
                    //.Include(i => i.User)
                    .Where(i => i.Group.Name.ToLower() == groupName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task RemoveUser(string groupName, string username)
        {
            var groupUser = await dbContext.GroupUsers
                .Where(i => i.Group.Name.ToLower() == groupName.ToLower() &&
                            i.User.UserName.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();

            dbContext.GroupUsers.Remove(groupUser);
            await dbContext.SaveChangesAsync();
        }
    }
}