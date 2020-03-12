using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Users
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