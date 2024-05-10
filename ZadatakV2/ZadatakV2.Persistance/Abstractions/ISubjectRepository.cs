using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface ISubjectRepository : IRepository<Subject>
    {        
        Task<bool> IsNameUniqueAsync(string name);     
    }
}
