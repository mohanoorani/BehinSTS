using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;
using ProjectX.IdentityContext.Application.Exceptions;
using ProjectX.IdentityContext.Application.Mappers.Groups;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using ProjectX.IdentityContext.Event.Groups;

namespace ProjectX.IdentityContext.Application.Services.Groups
{
    public partial class GroupService<TUser, TKey>
    {
        public async Task AddUser(GroupUserDto groupUserDto)
        {
            var group = await GetByName(groupUserDto.GroupName).ConfigureAwait(false);
            if (group == null) throw new EntityNotFoundException();

            var user = await userManager.FindByNameAsync(groupUserDto.Username);
            if (user == null) throw new EntityNotFoundException();

            await groupRepository.AddUser(
                    new GroupUser {GroupId = group.Id, UserId = user.Id.ToString()} ,
                    updaterId)
                .ConfigureAwait(false);

            loggerService.AddEvent(
                new GroupUserAddedEvent(group.Id, groupUserDto.GroupName, groupUserDto.Username));
        }

        public async Task<List<GroupUserDto>> GetAllUsers(string groupName)
        {
            var users = await groupRepository.GetAllUsers(groupName).ConfigureAwait(false);

            return users.Select(i => i?.ToGroupUserDto()).ToList();
        }

        public async Task RemoveUser(GroupUserDto groupUserDto)
        {
            var group = await GetByName(groupUserDto.GroupName).ConfigureAwait(false);
            if (group == null) throw new EntityNotFoundException();

            var user = await userManager.FindByNameAsync(groupUserDto.Username);
            if (user == null) throw new EntityNotFoundException();

            await groupRepository.RemoveUser(
                    new GroupUser { GroupId = group.Id, UserId = user.Id.ToString()},
                    updaterId)
                .ConfigureAwait(false);

            loggerService.AddEvent(new GroupUserRemovedEvent(group.Id, groupUserDto.GroupName, groupUserDto.Username));
        }
    }
}