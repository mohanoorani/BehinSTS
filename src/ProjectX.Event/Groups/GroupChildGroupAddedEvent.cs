using System;

namespace ProjectX.Event.Groups
{
    public class GroupChildGroupAddedEvent : BaseDomainEvent
    {
        public GroupChildGroupAddedEvent(Guid id, Guid childGroupId)
            : base(id)
        {
            ChildGroupId = childGroupId;
        }

        public Guid ChildGroupId { get; }
    }
}