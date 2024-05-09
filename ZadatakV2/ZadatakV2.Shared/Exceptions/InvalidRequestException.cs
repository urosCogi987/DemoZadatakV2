namespace ZadatakV2.Shared.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base()
        {
        }

        public InvalidRequestException(string message) : base(message)
        {
        }
    }
}
