using System;

namespace WashingTime.Exceptions
{
    public class UniqueConstraintViolationException : ArgumentException
    {
        public UniqueConstraintViolationException(string message)
            : base(message)
        {
        }
    }
}