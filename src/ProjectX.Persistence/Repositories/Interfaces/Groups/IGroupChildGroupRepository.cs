using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Domain.Entities.Groups;

namespace ProjectX.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task AddChildGroup(Guid parentGroupId, Guid childGroupId);

        Task<List<GroupChildGroup>> GetAllChildGroups(Guid parentGroupId);

        Task RemoveChildGroup(Guid parentGroupId, Guid childGroupId);
    }
}