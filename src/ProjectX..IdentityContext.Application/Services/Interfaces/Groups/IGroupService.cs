using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Groups
{
    public partial interface IGroupService
    {
        Task<GroupDto> Create(CreateGroupDto dto);
        
        Task<List<GroupDto>> GetAll();

        Task<GroupDto> GetByName(string name);

        Task Update(UpdateGroupDto dto);

        Task Clone(CloneGroupDto dto);

        Task Remove(string name);
    }
}