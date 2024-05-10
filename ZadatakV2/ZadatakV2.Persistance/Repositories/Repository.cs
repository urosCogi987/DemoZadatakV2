using Microsoft.EntityFrameworkCore;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)        
            => _dbContext = dbContext;

        public async Task<T?> GetItemByIdAsync(long id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetItemsAsync()
            => await _dbContext.Set<T>().ToListAsync();        

        public async Task AddItemAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();            
        }        

        public async Task UpdateItemAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }                
    }
}
