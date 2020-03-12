using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
{
    public class GroupDeletedEvent : BaseDomainEvent
    {
        public GroupDeletedEvent(Guid id) : base(id)
        {
        }
    }
}