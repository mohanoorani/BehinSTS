using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
{
    public class GroupDescriptionChangedEvent : BaseDomainEvent
    {
        public GroupDescriptionChangedEvent(Guid id, string oldValue, string newValue)
            : base(id)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string OldValue { get; }

        public string NewValue { get; }
    }
}