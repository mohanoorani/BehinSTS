﻿using System;

namespace ProjectX.IdentityContext.Domain.Entities.Groups
{
    public class GroupChildGroup
    {
        public Guid ChildGroupId { get; set; }
        
        public Guid ParentGroupId { get; set; }

        public Group ChildGroup { get; set; }
        
        public Group ParentGroup { get; set; }
    }
}
