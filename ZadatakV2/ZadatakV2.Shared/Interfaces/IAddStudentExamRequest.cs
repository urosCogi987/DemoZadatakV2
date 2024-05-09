namespace ZadatakV2.Shared.Interfaces
{
    public interface IAddStudentExamRequest
    {
        long StudentId { get; }
        long SubjectId { get; }
        int Mark { get; }
    }
}
