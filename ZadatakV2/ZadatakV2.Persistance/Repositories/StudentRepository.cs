using Microsoft.EntityFrameworkCore;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {        
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }        

        public async Task<bool> IsIndexUniqueAsync(string index)        
            => !(await _dbContext.Set<Student>().AnyAsync(student => student.Index == index));        
    }
}
