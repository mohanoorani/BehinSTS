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
            var groupUser = new GroupUserBuilder().Build();

            await AddUser(groupUser);

            var groupUsers = await GetAllUsers(groupUser.GroupName);

            groupUsers.Should().HaveCount(1);
        }
    }
}