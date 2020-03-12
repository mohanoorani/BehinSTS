using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Users
{
    public class UserUsernameChangedEvent : BaseDomainEvent
    {
        public UserUsernameChangedEvent(string id, string oldValue, string newValue)
            : base(Guid.Parse(id))
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string OldValue { get; }
        
        public string NewValue { get; }
    }
}