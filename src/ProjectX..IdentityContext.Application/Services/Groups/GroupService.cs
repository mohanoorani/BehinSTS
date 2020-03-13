using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectX.IdentityContext.Application.Dtos.Group;
using ProjectX.IdentityContext.Application.Exceptions;
using ProjectX.IdentityContext.Application.Mappers.Groups;
using ProjectX.IdentityContext.Application.Services.Interfaces.Groups;
using ProjectX.IdentityContext.Application.Services.Interfaces.Loggers;
using ProjectX.IdentityContext.Domain;
using ProjectX.IdentityContext.Domain.Entities.Groups;
using ProjectX.IdentityContext.Event.Groups;
using ProjectX.IdentityContext.Persistence.Repositories.Interfaces.Groups;

namespace ProjectX.IdentityContext.Application.Services.Groups
{
    public partial class GroupService<TUser, TKey> : IGroupService
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        private readonly IGroupRepository groupRepository;
        
        private readonly UserManager<TUser> userManager;

        private readonly ILoggerService loggerService;

        public GroupService(
            IGroupRepository groupRepository,
            UserManager<TUser> userManager,
            ILoggerService loggerService)
        {
            this.groupRepository = groupRepository;
            this.userManager = userManager;
            this.loggerService = loggerService;
        }

        public async Task<GroupDto> Create(CreateGroupDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new EntityNotFoundException(DomainResources.Group_Name_CanNotBeEmpty);

            var group = new Group {Name = dto.Name, Description = dto.Description, CreatorId = dto.CreatorId};

            await groupRepository.Add(group).ConfigureAwait(false);

            loggerService.AddEvent(new GroupCreatedEvent(group.Id));
            loggerService.AddEvent(new GroupNameChangedEvent(group.Id, null, dto.Name));
            loggerService.AddEvent(new GroupDescriptionChangedEvent(group.Id, null, dto.Description));

            return group.ToGroupDto();
        }

        public async Task<List<GroupDto>> GetAll()
        {
            var groups = await groupRepository.GetAll();

            return groups.Select(i => i?.ToGroupDto()).ToList();
        }

        public async Task<GroupDto> GetByName(string name)
        {
            var group = await groupRepository.GetByName(name).ConfigureAwait(false);

            return group?.ToGroupDto();
        }

        public async Task Update(UpdateGroupDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewName) || string.IsNullOrWhiteSpace(dto.Name))
                throw new EntityNotFoundException(DomainResources.Group_Name_CanNotBeEmpty);

            var oldGroup = await groupRepository.GetByName(dto.Name).ConfigureAwait(false);
            if (oldGroup == null)
                throw new EntityNotFoundException();

            var oldDescription = oldGroup.Description;

            await groupRepository.Update(dto.Name, dto.NewName, dto.Description, dto.UpdaterId).ConfigureAwait(false);

            if (!dto.Name.Equals(oldGroup.Name, StringComparison.InvariantCultureIgnoreCase))
                loggerService.AddEvent(new GroupNameChangedEvent(oldGroup.Id, dto.Name, dto.NewName));

            if (!oldDescription.Equals(oldGroup.Description, StringComparison.InvariantCultureIgnoreCase))
                loggerService.AddEvent(new GroupNameChangedEvent(oldGroup.Id, oldDescription, dto.Description));
        }

        public async Task Clone(CloneGroupDto dto)
        {
            var group = await GetByName(dto.Name).ConfigureAwait(false);
            if (group == null)
                throw new EntityNotFoundException();

            var createGroupDto = new CreateGroupDto
            {
                Name = dto.CloneName,
                Description = group.Description,
                CreatorId = dto.CreatorId
            };

            await Create(createGroupDto).ConfigureAwait(false);

            group.ChildGroups.ForEach(async i => await AddChildGroup(
                new GroupChildGroupDto
                {
                    ParentGroupName = dto.CloneName,
                    ChildGroupName = i.ChildGroupName
                }).ConfigureAwait(false));

            group.Users.ForEach(async i => await AddUser(
                new GroupUserDto
                {
                    GroupName = dto.CloneName,
                    Username = i.Username
                }).ConfigureAwait(false));
        }

        public async Task Remove(string name)
        {
            var group = await groupRepository.GetByName(name).ConfigureAwait(false);
            if (group == null) throw new EntityNotFoundException();

            loggerService.AddEvent(new GroupDeletedEvent(group.Id));

            await groupRepository.Remove(group);
        }
    }
}