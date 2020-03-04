using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjectX.IntegrationTest.Groups
{
    [Collection(nameof(TestFixtureCollection))]
    public partial class GroupsControllerIntegrationTest
    {
        public GroupsControllerIntegrationTest(TestFixture testFixture)
        {
            client = testFixture.Client;

            //var user = new
            //{
            //    UserName = "AdminUser",
            //    Email = "AdminUser@AdminUser.com",
            //    PhoneNumber = "00000",
            //    AccessFailedCount = 0
            //};

            //RegisterUser(user).GetAwaiter().GetResult();
        }

        private const string GroupsUrl = "api/groups";

        private readonly HttpClient client;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task TestPost_WhenNameIsEmpty_StatusCodeMustBeBadRequest(string name)
        {
            var groupDto = new GroupBuilder().WithName(name).BuildCreateGroupDto();

            await Create(groupDto, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task TestUpdate_WhenNameIsEmpty_StatusCodeMustBeBadRequest(string name)
        {
            var groupDto = new GroupBuilder().BuildCreateGroupDto();

            await Create(groupDto);

            var updateDto = new GroupBuilder()
                .WithName(name)
                .BuildUpdateGroupDto(groupDto.Name);

            await Update(updateDto, HttpStatusCode.BadRequest);

            await Delete(groupDto.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task TestUpdate_WhenNewNameIsEmpty_StatusCodeMustBeBadRequest(string name)
        {
            var groupDto = new GroupBuilder().BuildCreateGroupDto();

            await Create(groupDto);

            var updateDto = new GroupBuilder()
                .WithName(name)
                .BuildUpdateGroupDto(groupDto.Name);

            await Update(updateDto, HttpStatusCode.BadRequest);

            await Delete(groupDto.Name);
        }

        [Fact]
        public async Task TestDelete_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var groupDto = new GroupBuilder().BuildCreateGroupDto();

            await Create(groupDto);

            await Delete(groupDto.Name);

            await GetByName(groupDto.Name, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestDelete_WhenGroupDoesNotExists_StatusCodeMustBeBadRequest()
        {
            await Delete("Name", HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestGetAll_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var groups = await GetAll();

            groups.Should().HaveCount(2);

            var firstGroup = groups.First(i => i.Name == DbMigrationConstants.Group);
            firstGroup.Name.Should().Be(DbMigrationConstants.Group);
            firstGroup.Description.Should().Be(DbMigrationConstants.Group);

            var secondGroup = groups.First(i => i.Name == DbMigrationConstants.ChildGroup);
            secondGroup.Name.Should().Be(DbMigrationConstants.ChildGroup);
            secondGroup.Description.Should().Be(DbMigrationConstants.ChildGroup);

            await Delete(DbMigrationConstants.Group);
            await Delete(DbMigrationConstants.ChildGroup);
        }

        [Fact]
        public async Task TestPost_WhenEverythingIsOK_StatusCodeMustBeCreated()
        {
            var groupDto = new GroupBuilder().BuildCreateGroupDto();

            var group = await Create(groupDto);

            group.Name.Should().Be(groupDto.Name);
            group.Description.Should().Be(groupDto.Description);

            await Delete(groupDto.Name);
        }

        [Fact]
        public async Task TestUpdate_WhenEverythingIsOK_StatusCodeMustBeNoContent()
        {
            var groupDto = new GroupBuilder().BuildCreateGroupDto();

            await Create(groupDto);

            var updateDto = new GroupBuilder().WithName("GroupA").BuildUpdateGroupDto(groupDto.Name);

            await Update(updateDto);

            var group = await GetByName(updateDto.NewName);

            group.Name.Should().Be(updateDto.NewName);
            group.Description.Should().Be(updateDto.Description);

            await Delete(updateDto.NewName);
        }

        [Fact]
        public async Task TestUpdate_WhenGroupDoesNotExists_StatusCodeMustBeBadRequest()
        {
            var updateDto = new GroupBuilder().BuildUpdateGroupDto("NotExistGroup");

            await Update(updateDto, HttpStatusCode.BadRequest);
        }
    }
}