using System;

namespace ProjectX.IdentityContext.Application.Dtos.Group
{
    public class UpdateGroupDto
    {
        public string Name { get; set; }

        public string NewName { get; set; }

        public string Description { get; set; }
    }
}