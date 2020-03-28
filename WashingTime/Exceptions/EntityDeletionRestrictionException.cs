using System;

namespace WashingTime.Exceptions
{
    public class EntityDeletionRestrictionException : ArgumentException
    {
        public EntityDeletionRestrictionException(string message)
            : base(message)
        {
        }
    }
}