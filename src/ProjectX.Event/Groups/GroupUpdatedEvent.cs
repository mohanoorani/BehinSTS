using System;

namespace ProjectX.Event.Groups
{
    public class GroupUpdatedEvent : BaseDomainEvent
    {
        public GroupUpdatedEvent(Guid id, string oldName, string newName, string oldDescription, string newDescription)
            : base(id)
        {
            OldName = oldName;
            NewName = newName;
            OldDescription = oldDescription;
            NewDescription = newDescription;
        }

        public string OldName { get; }
        public string NewName { get; }
        public string OldDescription { get; }
        public string NewDescription { get; }
    }
}