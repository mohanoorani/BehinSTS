using System;

namespace ProjectX.Domain.Entities
{
    public class UserEvent
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public string EventValues { get; set; }

        public DateTime CreationDate { get; set; }
    }
}