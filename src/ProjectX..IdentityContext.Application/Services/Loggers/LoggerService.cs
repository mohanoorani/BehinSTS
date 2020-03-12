using System.Collections.Generic;
using ProjectX.Framework.Domain;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;

namespace ProjectX.IdentityContext.Application.Services.Loggers
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