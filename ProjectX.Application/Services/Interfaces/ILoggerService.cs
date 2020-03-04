using System.Collections.Generic;
using ProjectX.Event;

namespace ProjectX.Application.Services.Interfaces
{
    public interface ILoggerService
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddEvent(IDomainEvent domainEvent);
    }
}