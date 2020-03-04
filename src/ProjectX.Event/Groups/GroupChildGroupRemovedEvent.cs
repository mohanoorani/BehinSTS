using System;

namespace ProjectX.Event.Groups
{
    public class GroupChildGroupRemovedEvent : BaseDomainEvent
    {
        public GroupChildGroupRemovedEvent(Guid id, Guid childGroupId)
            : base(id)
        {
            ChildGroupId = childGroupId;
        }

        public Guid ChildGroupId { get; }
    }
}