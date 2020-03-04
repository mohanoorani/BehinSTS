using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjectX.IntegrationTest.Groups
{
    public partial class GroupsControllerIntegrationTest
    {
        [Fact]
        public async Task TestAddUser_WhenEverythingIsOk_StatusMustBeNoContent()
        {
            var user = new
            {
                UserName = "AdminUser",
                Email = "AdminUser@AdminUser.com",
                PhoneNumber = "00000",
                AccessFailedCount = 0
            };

            await RegisterUser(user);

            var groupUser = new GroupUserBuilder().WithGroupName(createdGroup.Name).WithUsername(user.UserName).Build();

            await AddUser(groupUser);

            var groupUsers = await GetAllUsers(groupUser.GroupName);

            groupUsers.Should().HaveCount(1);
        }
    }
}