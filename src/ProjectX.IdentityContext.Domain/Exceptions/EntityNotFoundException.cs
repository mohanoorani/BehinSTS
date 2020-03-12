using System;

namespace ProjectX.IdentityContext.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) :base(message)
        {
            
        }

        public EntityNotFoundException()
        {

        }
    }
}
