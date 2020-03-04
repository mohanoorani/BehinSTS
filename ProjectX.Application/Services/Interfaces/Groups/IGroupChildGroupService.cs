using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Application.Dtos.Group;

namespace ProjectX.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task AddChildGroup(GroupChildGroupDto @group);

        Task<List<GroupChildGroupDto>> GetAllChildGroups(string groupName);

        Task RemoveChildGroup(string name, string childGroupName);
    }
}