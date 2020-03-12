using System;

namespace Skoruba.IdentityServer4.Admin.Audit.IntegrationTests
{
    public class AuditBuilder
    {
        public ProjectX.IdentityContext.Domain.Entities.Audits.Audit Build()
        {
            return  new ProjectX.IdentityContext.Domain.Entities.Audits.Audit
            {
                EventTime = DateTime.Now,
                EventName = "FakeCreatedEvent",
                Data = "{\"UserName\":\"Mohammad.No\"}"
            };
        }
    }
}
