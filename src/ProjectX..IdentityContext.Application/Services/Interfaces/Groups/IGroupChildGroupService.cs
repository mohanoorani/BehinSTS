using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task AddChildGroup(GroupChildGroupDto group);

        Task<List<GroupChildGroupDto>> GetAllChildGroups(string groupName);

        Task RemoveChildGroup(string name, string childGroupName);
    }
}