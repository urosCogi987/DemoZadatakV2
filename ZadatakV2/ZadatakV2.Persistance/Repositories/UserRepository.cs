using Microsoft.EntityFrameworkCore;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<long> AddUserAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User?> FindUserByEmailAsync(string email)        
            => await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);

        public async Task<User?> FindUserByIdAsync(long id)
            => await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Id == id);

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Set<User>().Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
