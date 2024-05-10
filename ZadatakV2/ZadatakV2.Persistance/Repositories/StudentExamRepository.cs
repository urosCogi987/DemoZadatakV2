using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class StudentExamRepository : Repository<StudentExam>, IStudentExamRepository
    {        
        public StudentExamRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
