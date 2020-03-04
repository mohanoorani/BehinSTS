using System;

namespace ProjectX.Application.Exceptions
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
