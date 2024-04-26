using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IStudentRepository
    {
        Task<long> AddStudentAsync(Student student);
    }
}
