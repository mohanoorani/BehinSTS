using System;
using System.Collections.Generic;

namespace ProjectX.Application.Dtos.Group
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<GroupUserDto> Users { get; set; }

        public List<GroupChildGroupDto> ChildGroups { get; set; }
    }
}
