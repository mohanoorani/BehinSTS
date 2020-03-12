using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.IntegrationTest.Groups
{
    public partial class GroupsControllerIntegrationTest
    {
        private async Task<GroupDto> Create(CreateGroupDto model,
            HttpStatusCode responseStatusCode = HttpStatusCode.Created)
        {
            var response = await client.PostAsJsonAsync(GroupsUrl, model).ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.Created
                ? null
                : await response.Content.ReadAsAsync<GroupDto>();
        }

        private async Task<GroupDto> GetByName(string name,
            HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.GetAsync($"{GroupsUrl}/{name}").ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.OK
                ? null
                : await response.Content.ReadAsAsync<GroupDto>();
        }

        private async Task<List<GroupDto>> GetAll(HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.GetAsync($"{GroupsUrl}").ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.OK
                ? null
                : await response.Content.ReadAsAsync<List<GroupDto>>();
        }

        private async Task Update(
            UpdateGroupDto model,
            HttpStatusCode responseStatusCode = HttpStatusCode.NoContent)
        {
            var response = await client.PutAsJsonAsync($"{GroupsUrl}", model).ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);
        }

        private async Task<GroupDto> Clone(
            CloneGroupDto model,
            HttpStatusCode responseStatusCode = HttpStatusCode.Created)
        {
            var response = await client.PostAsJsonAsync($"{GroupsUrl}/{model.Name}/Clone", new {command = model})
                .ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.Created
                ? null
                : await response.Content.ReadAsAsync<GroupDto>();
        }

        private async Task Delete(string name, HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.DeleteAsync($"{GroupsUrl}/{name}");

            response.StatusCode.Should().Be(responseStatusCode);
        }

        private async Task AddUser(
            GroupUserDto model,
            HttpStatusCode responseStatusCode = HttpStatusCode.NoContent)
        {
            var response = await client.PutAsJsonAsync($"{GroupsUrl}/{model.GroupName}/Users", model)
                .ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);
        }

        private async Task<List<GroupUserDto>> GetAllUsers(
            string groupName,
            HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.GetAsync($"{GroupsUrl}/{groupName}/Users").ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.OK
                ? null
                : await response.Content.ReadAsAsync<List<GroupUserDto>>();
        }

        private async Task RemoveUser(
            string name,
            string username,
            HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client
                .DeleteAsync($"{GroupsUrl}/{name}/Users/{username}")
                .ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);
        }

        private async Task AddChildGroup(
            GroupChildGroupDto model,
            HttpStatusCode responseStatusCode = HttpStatusCode.NoContent)
        {
            var response = await client.PutAsJsonAsync($"{GroupsUrl}/{model.ParentGroupName}/ChildGroups", model)
                .ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);
        }

        private async Task<List<GroupChildGroupDto>> GetAllChildGroups(
            string groupName,
            HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.GetAsync($"{GroupsUrl}/{groupName}/ChildGroups").ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);

            return responseStatusCode != HttpStatusCode.OK
                ? null
                : await response.Content.ReadAsAsync<List<GroupChildGroupDto>>();
        }

        private async Task RemoveChildGroup(
            string parentGroupName,
            string childGroupName,
            HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            var response = await client.DeleteAsync($"{GroupsUrl}/{parentGroupName}/ChildGroups/{childGroupName}")
                .ConfigureAwait(false);

            response.StatusCode.Should().Be(responseStatusCode);
        }
    }
}