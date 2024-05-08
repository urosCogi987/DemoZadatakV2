using System.Data;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface ISubjectRepository
    {
        Task<long> AddSubjectAsync(Subject subject);
        Task<bool> IsNameUniqueAsync(string name);
        Task DeleteSubjectAsync(Subject subject);
        Task<Subject?> GetSubjectByIdAsync(long id);
    }
}
