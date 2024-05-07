namespace ZadatakV2.Shared.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
