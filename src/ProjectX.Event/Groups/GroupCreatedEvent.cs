using System;

namespace ProjectX.Event.Groups
{
    public class GroupCreatedEvent : BaseDomainEvent
    {
        public GroupCreatedEvent(Guid id, string name, string description)
            : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}