using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class StudentExamRepository : IStudentExamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentExamRepository(ApplicationDbContext dbContext)        
            => _dbContext = dbContext;
        
        public async Task<long> AddStudentExamAsync(StudentExam studentExam)
        {
            await _dbContext.Set<StudentExam>().AddAsync(studentExam);
            await _dbContext.SaveChangesAsync();
            return studentExam.Id;
        }
    }
}
