using System;

namespace ProjectX.Event.UserEvents
{
    public class UserUpdatedEvent : BaseDomainEvent
    {
        public UserUpdatedEvent(
            string id, 
            string oldUserName, 
            string newUserName, 
            string oldEmail, 
            string newEmail)
            : base(Guid.Parse(id))
        {
            OldUserName = oldUserName;
            NewUserName = newUserName;
            OldEmail = oldEmail;
            NewEmail = newEmail;
        }

        public string OldUserName { get; }

        public string NewUserName { get; }
        
        public string OldEmail { get; }
        
        public string NewEmail { get; }
    }
}