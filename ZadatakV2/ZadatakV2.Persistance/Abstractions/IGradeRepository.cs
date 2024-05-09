using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IGradeRepository
    {
        Task AddGradeAsync(Grade grade);
        Task<bool> DoesGradeExist(long studentId, long subjectId);
    }
}
