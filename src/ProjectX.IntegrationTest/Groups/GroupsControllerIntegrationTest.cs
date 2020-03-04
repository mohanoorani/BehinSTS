using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectX.Application.Dtos.Group;
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

            var groupDto = new GroupBuilder().WithName("TestGroup").BuildCreateGroupDto();
            var childGroupDto = new GroupBuilder().WithName("ChildGroup").BuildCreateGroupDto();
            
            createdGroup = Create(groupDto).GetAwaiter().GetResult();
            childGroup = Create(childGroupDto).GetAwaiter().GetResult();
        }

        private const string GroupsUrl = "api/groups";

        private readonly GroupDto createdGroup;

        private readonly GroupDto childGroup;

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

            var firstGroup = groups.First(i => i.Name == createdGroup.Name);
            firstGroup.Name.Should().Be(createdGroup.Name);
            firstGroup.Description.Should().Be(createdGroup.Description);

            var secondGroup = groups.First(i => i.Name == childGroup.Name);
            secondGroup.Name.Should().Be(childGroup.Name);
            secondGroup.Description.Should().Be(childGroup.Description);

            await Delete(createdGroup.Name);
            await Delete(childGroup.Name);
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