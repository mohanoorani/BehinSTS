using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups
{
    public partial interface IGroupRepository
    {
        Task AddUser(GroupUser groupUser, string UpdaterId);

        Task<List<GroupUser>> GetAllUsers(string groupName);

        Task RemoveUser(GroupUser groupUser, string UpdaterId);
    }
}