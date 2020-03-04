using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.Application.Dtos.Group;
using ProjectX.Application.Exceptions;
using ProjectX.Application.Mappers;
using ProjectX.Application.Services.Interfaces;
using ProjectX.Application.Services.Interfaces.Groups;
using ProjectX.Domain;
using ProjectX.Domain.Entities.Groups;
using ProjectX.Event.Groups;
using ProjectX.Persistence.Repositories.Interfaces.Groups;

namespace ProjectX.Application.Services.Groups
{
    public partial class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        private readonly ILoggerService loggerService;

        public GroupService(IGroupRepository groupRepository, ILoggerService loggerService)
        {
            this.groupRepository = groupRepository;
            this.loggerService = loggerService;
        }

        public async Task<GroupDto> Create(CreateGroupDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    throw new EntityNotFoundException(MessageConstants.NameCanNotBeNull);

                var group = new Group { Name = dto.Name, Description = dto.Description };

                await groupRepository.Add(group).ConfigureAwait(false);

                loggerService.AddEvent(new GroupCreatedEvent(group.Id, group.Name, group.Description));

                return group.ToGroupDto();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
            if (string.IsNullOrWhiteSpace(dto.NewName))
                throw new EntityNotFoundException(MessageConstants.NameCanNotBeNull);

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new EntityNotFoundException(MessageConstants.NameCanNotBeNull);

            var oldGroup = await groupRepository.GetByName(dto.Name).ConfigureAwait(false);
            if (oldGroup == null)
                throw new EntityNotFoundException();

            var oldDescription = oldGroup.Description;

            await groupRepository.Update(dto.Name, dto.NewName, dto.Description).ConfigureAwait(false);

            loggerService.AddEvent(
                new GroupUpdatedEvent(oldGroup.Id, dto.Name, dto.NewName, oldDescription, dto.Description));
        }

        public async Task Clone(CloneGroupDto dto)
        {
            var group = await GetByName(dto.Name).ConfigureAwait(false);
            if (group == null)
                throw new EntityNotFoundException();

            await Create(new CreateGroupDto {Name = dto.CloneName, Description = group.Description})
                .ConfigureAwait(false);

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