using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task AddUser(GroupUserDto groupUserDto);
        
        Task<List<GroupUserDto>> GetAllUsers(string groupName);

        Task RemoveUser(GroupUserDto groupUserDto);
    }
}