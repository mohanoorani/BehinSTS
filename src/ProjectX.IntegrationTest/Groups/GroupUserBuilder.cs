using ProjectX.Application.Dtos.Group;

namespace ProjectX.IntegrationTest.Groups
{
    public class GroupUserBuilder
    {
        private string groupName;

        private string username;

        public GroupUserBuilder()
        {
            groupName = DbMigrationConstants.Group;
            username = DbMigrationConstants.AdminUser;
        }

        public GroupUserDto Build()
        {
            return new GroupUserDto {GroupName = groupName, Username = username};
        }

        public GroupUserBuilder WithGroupName(string groupName)
        {
            this.groupName = groupName;

            return this;
        }

        public GroupUserBuilder WithUsername(string username)
        {
            this.username = username;

            return this;
        }
    }
}