using System.Linq;
using ProjectX.Application.Dtos.Group;
using ProjectX.Domain.Entities.Groups;

namespace ProjectX.Application.Mappers
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