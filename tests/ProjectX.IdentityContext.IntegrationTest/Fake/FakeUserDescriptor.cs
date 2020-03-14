using ProjectX.IdentityContext.Application.UserDescriptor;

namespace ProjectX.IdentityContext.IntegrationTest.Fake
{
    public class FakeUserDescriptor : IUserDescriptor
    {
        private static readonly string DefaultUserId = DbMigrationConstants.ActiveUserId;

        private static readonly string DefaultUsername = DbMigrationConstants.ActiveUsername;

        private static string UserId = DefaultUserId;

        private static string Username = DefaultUsername;

        public string GetUserAgent()
        {
            return "FireFox";
        }

        public string GetUserId()
        {
            return UserId;
        }

        public string GetUsername()
        {
            return Username;
        }

        public string GetUserIp()
        {
            return "192.168.183.153";
        }

        public static void ResetUser()
        {
            UserId = DefaultUserId;
            Username = DefaultUsername;
        }

        public static void SetUser(string userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}