using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task AddUser(string groupName, string username);

        Task<List<GroupUser>> GetAllUsers(string groupName);

        Task RemoveUser(string groupName, string username);
    }
}