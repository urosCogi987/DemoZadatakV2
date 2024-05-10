using Microsoft.EntityFrameworkCore;
using System.Data;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {        
        public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }                

        public async Task<bool> IsNameUniqueAsync(string name)
            => !(await _dbContext.Set<Subject>().AnyAsync(subject => subject.Name == name));
    }
}
