using System.Collections.Generic;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Application.Services.Interfaces.Loggers
{
    public interface ILoggerService
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddEvent(IDomainEvent domainEvent);
    }
}