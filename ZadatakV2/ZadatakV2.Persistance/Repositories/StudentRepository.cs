using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<long> AddStudentAsync(Student student)
        {
            await _dbContext.Set<Student>().AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }
    }
}
