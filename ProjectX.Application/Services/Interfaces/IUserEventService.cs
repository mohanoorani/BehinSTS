using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Services.Interfaces
{
    public interface IUserEventService
    {
        void Add(string eventName, string values);
        
        Task<List<UserEvent>> GetAll();

        Task RemoveAll();
    }
}