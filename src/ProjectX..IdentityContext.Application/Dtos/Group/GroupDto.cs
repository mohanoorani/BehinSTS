using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProjectX.IdentityContext.Application.Dtos.Group
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }

        public string? UpdaterId { get; set; }

        public IdentityUser Updater { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public List<GroupUserDto> Users { get; set; }

        public List<GroupChildGroupDto> ChildGroups { get; set; }
    }
}
