using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProjectX.IdentityContext.Domain.Entities.Audits
{
    public class Audit
    {
        public Guid Id { get; set; }

        public string Data { get; set; }

        public Guid AggregateId { get; set; }

        public string EventName { get; set; }

        public string Ip { get; set; }

        public DateTime EventTime { get; set; }

        public string UserAgent { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}