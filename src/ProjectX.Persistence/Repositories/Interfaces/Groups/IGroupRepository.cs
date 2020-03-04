using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Domain.Entities.Groups;

namespace ProjectX.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task Add(Group group);

        Task<List<Group>> GetAll();

        Task<Group> GetByName(string name);

        Task Update(string name, string newName, string description);

        //Task Clone(string name, string clonedName);

        Task Remove(Group group);
    }
}