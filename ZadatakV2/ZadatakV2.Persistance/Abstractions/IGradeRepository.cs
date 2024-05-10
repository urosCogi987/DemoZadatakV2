using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IGradeRepository : IRepository<Grade>
    {        
        Task<bool> DoesGradeExist(long studentId, long subjectId);
    }
}
