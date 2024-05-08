using Microsoft.EntityFrameworkCore;
using System.Data;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubjectRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<long> AddSubjectAsync(Subject subject)
        {
            await _dbContext.Set<Subject>().AddAsync(subject);
            await _dbContext.SaveChangesAsync();
            return subject.Id;
        }

        public async Task DeleteSubjectAsync(Subject subject)
        {
            _dbContext.Set<Subject>().Remove(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(long id)
            => await _dbContext.Set<Subject>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> IsNameUniqueAsync(string name)
            => !(await _dbContext.Set<Subject>().AnyAsync(subject => subject.Name == name));
    }
}
