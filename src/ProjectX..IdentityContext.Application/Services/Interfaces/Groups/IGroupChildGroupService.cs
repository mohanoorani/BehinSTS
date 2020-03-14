using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task AddChildGroup(GroupChildGroupDto childGroupDto);

        Task<List<GroupChildGroupDto>> GetAllChildGroups(string groupName);

        Task RemoveChildGroup(GroupChildGroupDto childGroupDto);
    }
}