namespace ZadatakV2.Shared.Exceptions
{
    public sealed class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException() : base()
        {            
        }

        public UniqueConstraintViolationException(string message) : base(message) 
        {            
        }
    }
}
