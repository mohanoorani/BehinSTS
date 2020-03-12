using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;
using ProjectX.IdentityContext.Application.Mappers.Groups;
using ProjectX.IdentityContext.Event.Groups;

namespace ProjectX.IdentityContext.Application.Services.Groups
{
    public partial class GroupService
    {
        public async Task AddUser(GroupUserDto groupUser)
        {
            var group = await GetByName(groupUser.GroupName).ConfigureAwait(false);

            await groupRepository.AddUser(groupUser.GroupName, groupUser.Username)
                .ConfigureAwait(false);

            loggerService.AddEvent(new GroupUserAddedEvent(group.Id, groupUser.GroupName, groupUser.Username));
        }

        public async Task<List<GroupUserDto>> GetAllUsers(string groupName)
        {
            var users = await groupRepository.GetAllUsers(groupName).ConfigureAwait(false);

            return users.Select(i => i?.ToGroupUserDto()).ToList();
        }

        public async Task RemoveUser(string name, string username)
        {
            var groupUserDto = new GroupUserDto {GroupName = name, Username = username};
            var group = await GetByName(groupUserDto.GroupName).ConfigureAwait(false);

            await groupRepository.RemoveUser(groupUserDto.GroupName, groupUserDto.Username)
                .ConfigureAwait(false);

            loggerService.AddEvent(new GroupUserRemovedEvent(
                group.Id, groupUserDto.GroupName, groupUserDto.Username));
        }
    }
}