using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Application.Dtos.Group;

namespace ProjectX.Application.Services.Interfaces.Groups
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