using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Domain.Entities;

namespace ProjectX.Persistence.Repositories.Interfaces
{
    public interface IUserEventRepository
    {
        Task<int> Add(UserEvent log);

        Task<List<UserEvent>> GetAll();

        Task RemoveAll();
    }
}