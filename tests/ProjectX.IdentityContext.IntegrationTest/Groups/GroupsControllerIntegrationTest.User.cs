using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Faker;
using FluentAssertions;
using Xunit;

namespace ProjectX.IdentityContext.IntegrationTest.Groups
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
            groupUsers.First().GroupName.Should().Be(groupUser.GroupName);
            groupUsers.First().Username.Should().Be(groupUser.Username);

            await RemoveUser(groupUser.GroupName, groupUser.Username);
        }
        
        [Fact]
        public async Task TestAddUser_WhenUserDoesNotExists_StatusMustBeBadRequest()
        {
            var groupUser = new GroupUserBuilder().WithUsername(Name.First()).Build();

            await AddUser(groupUser, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestAddUser_WhenGroupDoesNotExists_StatusMustBeBadRequest()
        {
            var groupUser = new GroupUserBuilder().WithGroupName(Name.First()).Build();

            await AddUser(groupUser, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestGetAllUser_WhenNoUserAdded_StatusCodeMustBeOK()
        {
            var users = await GetAllUsers(DbMigrationConstants.Group);

            users.Should().HaveCount(0);
        }

        [Fact]
        public async Task TestGetAllUser_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var userDto = new GroupUserBuilder().Build();

            await AddUser(userDto);

            var users = await GetAllUsers(userDto.GroupName);

            users.Should().HaveCount(1);

            users.First().GroupName.Should().Be(userDto.GroupName);
            users.First().Username.Should().Be(userDto.Username);

            await RemoveUser(userDto.GroupName, userDto.Username);
        }

        [Fact]
        public async Task TestRemoveUser_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var userDto = new GroupUserBuilder().Build();

            await AddUser(userDto);

            await RemoveUser(userDto.GroupName, userDto.Username);

            var users = await GetAllUsers(userDto.GroupName);

            users.Should().HaveCount(0);
        }

        [Fact]
        public async Task TestRemoveUser_WhenUserDoesNotExists_StatusCodeMustBeOK()
        {
            await RemoveUser(DbMigrationConstants.Group, Name.First(),
                HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestRemoveUser_WhenGroupDoesNotExists_StatusCodeMustBeOK()
        {
            await RemoveUser(Name.First(), DbMigrationConstants.Group,
                HttpStatusCode.BadRequest);
        }
    }
}