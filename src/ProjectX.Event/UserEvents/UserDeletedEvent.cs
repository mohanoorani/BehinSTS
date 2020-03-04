using System;

namespace ProjectX.Event.UserEvents
{
    public class UserDeletedEvent : BaseDomainEvent
    {
        public UserDeletedEvent(string id, string userName)
            : base(Guid.Parse(id))
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}