using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Application.Dtos.Group;
using ProjectX.Application.Services.Interfaces.Groups;
using Skoruba.IdentityServer4.Admin.Api.ExceptionHandling;
using Skoruba.IdentityServer4.Admin.Api.Filters;

namespace Skoruba.IdentityServer4.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json", "application/problem+json")]
    [ServiceFilter(typeof(DomainEventLoggerFilter))]
    //[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> Get()
        {
            var groups = await groupService.GetAll().ConfigureAwait(false);

            return Ok(groups);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<GroupDto>> Get(string name)
        {
            var group = await groupService.GetByName(name).ConfigureAwait(false);

            if (group == null) return NotFound();
            
            return Ok(group);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<GroupDto>> Post(CreateGroupDto dto)
        {
            var group = await groupService.Create(dto).ConfigureAwait(false);

            return CreatedAtAction(nameof(Get), new { name = dto.Name }, group);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateGroupDto dto)
        {
            await groupService.Update(dto).ConfigureAwait(false);

            return NoContent();
        }

        [HttpPost("{name}/Clone")]
        public async Task<ActionResult> Clone(CloneGroupDto dto)
        {
            await groupService.Clone(dto).ConfigureAwait(false);

            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            await groupService.Remove(name).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("{name}/ChildGroups")]
        public async Task<ActionResult<GroupChildGroupDto>> GetAllChildGroups(string name)
        {
            var group = await groupService.GetAllChildGroups(name).ConfigureAwait(false);

            return Ok(group);
        }

        [HttpPut("{name}/ChildGroups")]
        public async Task<ActionResult> AddChildGroup(GroupChildGroupDto dto)
        {
            await groupService.AddChildGroup(dto).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{name}/ChildGroups/{childGroupName}")]
        public async Task<ActionResult> DeleteChildGroup(string name, string childGroupName)
        {
            await groupService.RemoveChildGroup(name, childGroupName).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet("{name}/Users")]
        public async Task<ActionResult<GroupUserDto>> GetAllUsers(string name)
        {
            var group = await groupService.GetAllUsers(name).ConfigureAwait(false);

            return Ok(group);
        }

        [HttpPut("{name}/Users")]
        public async Task<ActionResult> AddUser(GroupUserDto dto)
        {
            await groupService.AddUser(dto).ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{name}/Users/{userName}")]
        public async Task<ActionResult> DeleteUser(GroupUserDto dto)
        {
            await groupService.RemoveUser(dto).ConfigureAwait(false);

            return Ok();
        }
    }
}