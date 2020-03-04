using System;
using System.Collections.Generic;

namespace ProjectX.Domain.Entities.Groups
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

        public string Name { get; set; }

        public string Description { get; set; }

        public List<GroupChildGroup> ChildGroups { get; set; }

        public List<GroupChildGroup> ParentGroups { get; set; }

        public List<GroupUser> Users { get; set; }
    }
}
