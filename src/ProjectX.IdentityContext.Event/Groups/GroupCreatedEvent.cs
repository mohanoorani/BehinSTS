using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
{
    public class GroupCreatedEvent : BaseDomainEvent
    {
        public GroupCreatedEvent(Guid id) : base(id)
        {
        }
    }
}