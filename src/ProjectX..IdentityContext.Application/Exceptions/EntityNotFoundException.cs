using System;

namespace ProjectX.IdentityContext.Application.Exceptions
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
