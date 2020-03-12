using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
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