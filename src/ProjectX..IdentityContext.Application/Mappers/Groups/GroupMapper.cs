using System.Linq;
using ProjectX.IdentityContext.Application.Dtos.Group;
using ProjectX.IdentityContext.Domain.Entities.Groups;

namespace ProjectX.IdentityContext.Application.Mappers.Groups
{
    public static class GroupMapper
    {
        public static GroupDto ToGroupDto(this Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreatorId = group.CreatorId,
                Created = group.Created,
                UpdaterId = group.UpdaterId,
                Updated = group.Updated,
                Creator = group.Creator,
                Updater = group.Updater,
                Users = group.Users.Select(i => i.ToGroupUserDto()).ToList(),
                ChildGroups = group.ChildGroups.Select(i => i.ToGroupChildGroupDto()).ToList()
            };
        }
        public static GroupUserDto ToGroupUserDto(this GroupUser groupUser)
        {
            return new GroupUserDto
            {
                GroupName = groupUser.Group.Name,
                Username = groupUser.User.UserName
            };
        }

        public static GroupChildGroupDto ToGroupChildGroupDto(this GroupChildGroup groupChildGroup)
        {
            return new GroupChildGroupDto
            {
                ChildGroupName = groupChildGroup.ChildGroup?.Name,
                ParentGroupName = groupChildGroup.ParentGroup?.Name
            };
        }
    }
}