using System;

namespace ProjectX.Domain.Entities.Groups
{
    public class GroupChildGroup
    {
        public Guid ChildGroupId { get; set; }

        public Group ChildGroup { get; set; }
        
        public Group ParentGroup { get; set; }
    }
}
