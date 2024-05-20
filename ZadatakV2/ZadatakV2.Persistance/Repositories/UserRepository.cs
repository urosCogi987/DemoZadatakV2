using Microsoft.EntityFrameworkCore;
using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {        
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }       

        public async Task<User?> FindUserByEmailAsync(string email)        
            => await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);        
        
        public async Task<bool> IsEmailUniqueAsync(string email)
            => !(await _dbContext.Set<User>().AnyAsync(user => user.Email == email));       

        public async Task<bool> IsEmailVerified(string  email)
            => await _dbContext.Set<User>().AnyAsync(user => user.Email == email && user.IsEmailVerified == true);
       
        public async Task<bool> IsUserBlocked(long id)
            => await _dbContext.Set<User>().AnyAsync(user => user.Id == id && user.IsBlocked == true);
    }
}
