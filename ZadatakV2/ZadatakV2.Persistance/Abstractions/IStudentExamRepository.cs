using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IStudentExamRepository
    {
        Task<long> AddStudentExamAsync(StudentExam studentExam);
    }
}
