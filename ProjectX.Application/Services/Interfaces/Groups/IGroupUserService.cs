using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Application.Dtos.Group;

namespace ProjectX.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task AddUser(GroupUserDto groupUser);
        
        Task<List<GroupUserDto>> GetAllUsers(string groupName);

        Task RemoveUser(GroupUserDto groupUser);
    }
}