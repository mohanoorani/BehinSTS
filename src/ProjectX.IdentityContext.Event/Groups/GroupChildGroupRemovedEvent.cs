using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
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