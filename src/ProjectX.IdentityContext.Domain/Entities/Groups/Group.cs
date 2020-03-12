using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProjectX.IdentityContext.Domain.Entities.Groups
{
    public class Group
    {
        public Group()
        {
            ChildGroups = new List<GroupChildGroup>();
            ParentGroups = new List<GroupChildGroup>();
            Users = new List<GroupUser>();
        }

        public Guid Id { get; set; }

        public string CreatorId { get; set; }

        public IdentityUser Creator { get; set; }
        
        public string? UpdaterId { get; set; }
        
        public IdentityUser Updater { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<GroupChildGroup> ChildGroups { get; set; }

        public List<GroupChildGroup> ParentGroups { get; set; }

        public List<GroupUser> Users { get; set; }
    }
}
