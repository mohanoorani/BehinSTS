using System;

namespace ProjectX.IdentityContext.Application.Dtos.Group
{
    public class CreateGroupDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }
    }
}
