using Skoruba.IdentityServer4.Admin.Audit.Worker;
using Xunit;

namespace Skoruba.IdentityServer4.Admin.Audit.IntegrationTests
{
    public class WorkerIntegrationTests
    {
        [Fact]
        public void TestSendMessage_WhenEverythingIsOk_MustBeOK()
        {
            var audit = new AuditBuilder().Build();

            //Helper.SendToQueue(audit);
            Helper.ReceiveMessages();
        }
    }
}