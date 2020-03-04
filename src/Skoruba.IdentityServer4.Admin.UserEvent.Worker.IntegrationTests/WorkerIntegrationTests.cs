using System;
using FluentAssertions;
using Xunit;

namespace Skoruba.IdentityServer4.Admin.UserEvent.Worker.IntegrationTests
{
    public class WorkerIntegrationTests
    {
        [Fact]
        public void TestSendMessage_WhenEverythingIsOk_MustBeOK()
        {
           var userEvent = new UserEventBuilder().Build();

           //Helper.SendToQueue(userEvent);
           Helper.RecieveMessages();


        }
    }
}
