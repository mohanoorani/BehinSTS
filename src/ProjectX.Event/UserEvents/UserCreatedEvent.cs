using System;

namespace ProjectX.Event.UserEvents
{
    public class UserCreatedEvent : BaseDomainEvent
    {
        public UserCreatedEvent(string id, string userName)
            : base(Guid.Parse(id))
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}