using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectX.Domain.Entities.Groups;
using ProjectX.Persistence.Repositories.Interfaces.Groups;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace ProjectX.Persistence.Repositories.Groups
{
    public partial class GroupRepository<TDbContext, TUser, TKey> : IGroupRepository
        where TDbContext : DbContext, IAdminLogDbContext
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        private readonly TDbContext dbContext;

        private readonly UserManager<TUser> userManager;

        public GroupRepository(TDbContext dbContext, UserManager<TUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task Add(Group group)
        {
            dbContext.Groups.Add(group);
            await dbContext.SaveChangesAsync();
        }

        public Task<List<Group>> GetAll()
        {
            return dbContext.Groups.OrderBy(i => i.Name).ToListAsync();
        }

        public Task<Group> GetByName(string name)
        {
            return dbContext.Groups
                .Include(i => i.ChildGroups)
                .Include(i => i.ParentGroups)
                .Include(i => i.Users).ThenInclude(i => i.User)
                .FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());
        }

        public async Task Update(string name, string newName, string description)
        {
            var group = await GetByName(name).ConfigureAwait(false);
            if (group == null) throw new Exception();
            
            group.Name = newName;
            group.Description = description;

            await dbContext.SaveChangesAsync();
        }

        //public async Task Clone(string name, string clonedName)
        //{
        //    var group = await GetByName(name).ConfigureAwait(false);

        //    var clonedGroup = new Group
        //    {
        //        Name = clonedName,
        //        Description = group.Description
        //    };

        //    clonedGroup
        //        .Users
        //        .AddRange(group.Users.Select(i => new GroupUser {UserId = i.UserId}));

        //    clonedGroup
        //        .ChildGroups
        //        .AddRange(group.ChildGroups.Select(i => new GroupChildGroup {ChildGroupId = i.ChildGroupId}));

        //    await dbContext.SaveChangesAsync();
        //}

        public Task Remove(Group group)
        {
            dbContext.Groups.Remove(group);
            return dbContext.SaveChangesAsync();
        }
    }
}