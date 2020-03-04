using System;

namespace Skoruba.IdentityServer4.Admin.UserEvent.Worker.IntegrationTests
{
    public class UserEventBuilder
    {
        public ProjectX.Domain.Entities.UserEvent Build()
        {
            return  new ProjectX.Domain.Entities.UserEvent
            {
                CreationDate = DateTime.Now,
                EventName = "FakeCreatedEvent",
                EventValues = "{\"UserName\":\"Mohammad.No\"}"
            };
        }
    }
}
