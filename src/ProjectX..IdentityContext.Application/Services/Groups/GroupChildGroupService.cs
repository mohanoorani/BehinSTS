using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.IdentityContext.Application.Dtos.Group;
using ProjectX.IdentityContext.Application.Exceptions;
using ProjectX.IdentityContext.Application.Mappers.Groups;
using ProjectX.IdentityContext.Domain;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using ProjectX.IdentityContext.Event.Groups;

namespace ProjectX.IdentityContext.Application.Services.Groups
{
    public partial class GroupService<TUser, TKey>
    {
        public async Task AddChildGroup(GroupChildGroupDto childGroupDto)
        {
            var parentGroup = await GetByName(childGroupDto.ParentGroupName).ConfigureAwait(false);
            if (parentGroup == null) throw new EntityNotFoundException();

            var childGroup = await GetByName(childGroupDto.ChildGroupName).ConfigureAwait(false);
            if (childGroup == null) throw new EntityNotFoundException();

            if (parentGroup.Id == childGroup.Id)
                throw new Exception(DomainResources.Group_CanNotAddGroupToItself);

            await groupRepository.AddChildGroup(
                    new GroupChildGroup {ParentGroupId = parentGroup.Id, ChildGroupId = childGroup.Id},
                    updaterId)
                .ConfigureAwait(false);

            loggerService.AddEvent(
                new GroupChildGroupAddedEvent(parentGroup.Id, childGroup.Id));
        }

        public async Task<List<GroupChildGroupDto>> GetAllChildGroups(string groupName)
        {
            var group = await GetByName(groupName).ConfigureAwait(false);
            if (group == null) return new List<GroupChildGroupDto>();

            var childGroups = await groupRepository.GetAllChildGroups(group.Id).ConfigureAwait(false);

            return childGroups.Select(i => i?.ToGroupChildGroupDto()).ToList();
        }

        public async Task RemoveChildGroup(GroupChildGroupDto childGroupDto)
        {
            var parentGroup = await GetByName(childGroupDto.ParentGroupName).ConfigureAwait(false);
            if (parentGroup == null) throw new EntityNotFoundException();

            var childGroup = await GetByName(childGroupDto.ChildGroupName).ConfigureAwait(false);
            if (childGroup == null) throw new EntityNotFoundException();

            await groupRepository.RemoveChildGroup(
                    new GroupChildGroup {ParentGroupId = parentGroup.Id, ChildGroupId = childGroup.Id},
                    updaterId)
                .ConfigureAwait(false);

            loggerService.AddEvent(new GroupChildGroupRemovedEvent(parentGroup.Id, childGroup.Id));
        }
    }
}