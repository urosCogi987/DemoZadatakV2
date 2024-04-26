namespace ZadatakV2.Shared.Interfaces
{
    public interface IAddStudentRequest
    {
        string Name { get; }
        string Surname { get; }
        string Index { get; }
        string Email { get; }
    }
}
