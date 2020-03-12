using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task Add(Group group);

        Task<List<Group>> GetAll();

        Task<Group> GetByName(string name);

        Task Update(string name, string newName, string description, string updaterId);

        Task Remove(Group group);
    }
}