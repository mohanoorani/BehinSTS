using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProjectX.Event
{
    public interface IDomainEvent
    {
        Dictionary<string, string> Flatten();
    }

    public class BaseDomainEvent : IDomainEvent
    {
        public BaseDomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; }

        public virtual Dictionary<string, string> Flatten()
        {
            var events = new Dictionary<string, string>();

            foreach (var property in GetType().GetProperties())
            {
                events.Add(property.Name, property.GetValue(this, null)?.ToString());
            }

            return events;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Flatten());
        }
    }
}