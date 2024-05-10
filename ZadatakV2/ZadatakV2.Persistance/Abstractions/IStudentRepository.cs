using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IStudentRepository : IRepository<Student>
    {        
        Task<bool> IsIndexUniqueAsync(string index);
    }
}
