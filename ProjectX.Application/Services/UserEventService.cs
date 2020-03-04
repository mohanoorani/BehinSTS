using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectX.Application.Services.Interfaces;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Repositories.Interfaces;

namespace ProjectX.Application.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository userEventRepository;

        public UserEventService(IUserEventRepository userEventRepository)
        {
            this.userEventRepository = userEventRepository;
        }

        public void Add(string eventName, string values)
        {
            userEventRepository.Add(new UserEvent
            {
                Id = Guid.NewGuid(),
                EventName = eventName,
                EventValues = values,
                CreationDate = DateTime.Now
            });
        }

        public Task<List<UserEvent>> GetAll()
        {
            return userEventRepository.GetAll();
        }

        public Task RemoveAll()
        {
            return userEventRepository.RemoveAll();
        }
    }
}