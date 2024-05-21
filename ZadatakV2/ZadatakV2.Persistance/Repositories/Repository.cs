using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
            
        public async Task<T?> GetItemByIdAsync(long id)
        {
            T? entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity is not null) 
                _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
            => await _dbContext.Set<T>().AsNoTracking().ToListAsync();        

        public async Task<T> AddItemAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }        

        public async Task UpdateItemAsync(T entity)
        {
            _dbContext.Set<T>().Attach(entity);            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(T entity)
        {            
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }                

        public async Task DeleteItemsRangeAsync(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IDbTransaction BeginTransaction()
        {
            var transaction = _dbContext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

    }
}
