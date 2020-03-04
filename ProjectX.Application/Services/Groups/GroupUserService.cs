using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.Application.Dtos.Group;
using ProjectX.Application.Mappers;
using ProjectX.Event.Groups;

namespace ProjectX.Application.Services.Groups
{
    public partial class GroupService
    {
        public async Task AddUser(GroupUserDto groupUser)
        {
            var group = await GetByName(groupUser.GroupName).ConfigureAwait(false);

            await groupRepository.AddUser(groupUser.GroupName, groupUser.Username).ConfigureAwait(false);
            
            loggerService.AddEvent(new GroupUserAddedEvent(group.Id, groupUser.GroupName, groupUser.Username));
        }

        public async Task<List<GroupUserDto>> GetAllUsers(string groupName)
        {
            var users = await groupRepository.GetAllUsers(groupName).ConfigureAwait(false);

            return users.Select(i => i?.ToGroupUserDto()).ToList();
        }

        public async Task RemoveUser(GroupUserDto groupUser)
        {
            var group = await GetByName(groupUser.GroupName).ConfigureAwait(false);
            
            await groupRepository.RemoveUser(groupUser.GroupName, groupUser.Username).ConfigureAwait(false);

            loggerService.AddEvent(new GroupUserRemovedEvent(group.Id, groupUser.GroupName, groupUser.Username));
        }
    }
}