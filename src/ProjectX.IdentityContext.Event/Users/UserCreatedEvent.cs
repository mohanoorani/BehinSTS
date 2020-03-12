using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Users
{
    public class UserCreatedEvent : BaseDomainEvent
    {
        public UserCreatedEvent(string id) : base(Guid.Parse(id))
        {
        }
    }
}