using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Faker;
using FluentAssertions;
using ProjectX.IdentityContext.Application.Dtos.Group;
using Xunit;

namespace ProjectX.IdentityContext.IntegrationTest.Groups
{
    public partial class GroupsControllerIntegrationTest
    {
        [Fact]
        public async Task TestAddChildGroup_WhenParentGroupDoesNotExists_StatusCodeMustBeNotFound()
        {
            var childGroupDto = new GroupChildGroupBuilder().WithParentGroupName("NotExists").Build();

            await AddChildGroup(childGroupDto, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestAddChildGroup_WhenChildGroupDoesNotExists_StatusCodeMustBeNotFound()
        {
            var childGroupDto = new GroupChildGroupBuilder().WithChildGroupName("NotExists").Build();

            await AddChildGroup(childGroupDto, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestAddChildGroup_WhenAddGroupToItself_StatusCodeMustBeBadRequest()
        {
            var childGroupDto = new GroupChildGroupBuilder()
                .WithParentGroupName(DbMigrationConstants.Group)
                .WithChildGroupName(DbMigrationConstants.Group)
                .Build();

            await AddChildGroup(childGroupDto, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestAddChildGroup_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var childGroupDto = new GroupChildGroupBuilder().Build();

            await AddChildGroup(childGroupDto);

            var childGroups = await GetAllChildGroups(childGroupDto.ParentGroupName);

            childGroups.Should().HaveCount(1);

            Enumerable.First<GroupChildGroupDto>(childGroups).ParentGroupName.Should().Be(childGroupDto.ParentGroupName);
            Enumerable.First<GroupChildGroupDto>(childGroups).ChildGroupName.Should().Be(childGroupDto.ChildGroupName);

            await RemoveChildGroup(childGroupDto.ParentGroupName, childGroupDto.ChildGroupName);
        }

        [Fact]
        public async Task TestGetAllChildGroup_WhenNoChildGroupAdded_StatusCodeMustBeOK()
        {
            var childGroups = await GetAllChildGroups(DbMigrationConstants.Group);

            childGroups.Should().HaveCount(0);
        }

        [Fact]
        public async Task TestGetAllChildGroup_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var childGroupDto = new GroupChildGroupBuilder().Build();

            await AddChildGroup(childGroupDto);

            var childGroups = await GetAllChildGroups(childGroupDto.ParentGroupName);

            childGroups.Should().HaveCount(1);

            Enumerable.First<GroupChildGroupDto>(childGroups).ParentGroupName.Should().Be(childGroupDto.ParentGroupName);
            Enumerable.First<GroupChildGroupDto>(childGroups).ChildGroupName.Should().Be(childGroupDto.ChildGroupName);

            await RemoveChildGroup(childGroupDto.ParentGroupName, childGroupDto.ChildGroupName);
        }

        [Fact]
        public async Task TestRemoveChildGroup_WhenEverythingIsOK_StatusCodeMustBeOK()
        {
            var childGroupDto = new GroupChildGroupBuilder().Build();

            await AddChildGroup(childGroupDto);

            await RemoveChildGroup(childGroupDto.ParentGroupName, childGroupDto.ChildGroupName);

            var childGroups = await GetAllChildGroups(childGroupDto.ParentGroupName);

            childGroups.Should().HaveCount(0);
        }

        [Fact]
        public async Task TestRemoveChildGroup_WhenChildGroupDoesNotExists_StatusCodeMustBeOK()
        {
            await RemoveChildGroup(DbMigrationConstants.Group, Name.First(), 
                HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestRemoveChildGroup_WhenParentGroupDoesNotExists_StatusCodeMustBeOK()
        {
            await RemoveChildGroup(Name.First(), DbMigrationConstants.Group, 
                HttpStatusCode.BadRequest);
        }
    }
}