using System;
using ProjectX.Framework.Domain;

namespace ProjectX.IdentityContext.Event.Groups
{
    public class GroupUserAddedEvent : BaseDomainEvent
    {
        public GroupUserAddedEvent(Guid id, string groupName, string userName)
            : base(id)
        {
            GroupName = groupName;
            UserName = userName;
        }

        public string GroupName { get; }
        
        public string UserName { get; }
    }
}