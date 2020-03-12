using Microsoft.AspNetCore.Identity;

namespace ProjectX.IdentityContext.Domain.Entities.Groups
{
    public class GroupUser
    {
        public string UserId { get; set; }

        public Group Group { get; set; }

        public IdentityUser User { get; set; }
    }
}