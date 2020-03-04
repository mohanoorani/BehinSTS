using System.Collections.Generic;
using ProjectX.Application.Services.Interfaces;
using ProjectX.Event;

namespace ProjectX.Application.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly List<IDomainEvent> domainEvents;

        public LoggerService()
        {
            domainEvents = new List<IDomainEvent>();
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

        public void AddEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }
    }
}