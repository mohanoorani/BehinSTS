using System;

namespace ProjectX.Event.Groups
{
    public class GroupDeletedEvent : BaseDomainEvent
    {
        public GroupDeletedEvent(Guid id)
            : base(id)
        {
        }
    }
}