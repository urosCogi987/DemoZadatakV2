namespace ZadatakV2.Shared.Interfaces
{
    public interface IAddGradeRequest
    {
        long StudentId { get; }
        long SubjectId { get; }
        int Value { get; }        
    }
}
