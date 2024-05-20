using Microsoft.EntityFrameworkCore;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class VerificationTokenRepository : Repository<VerificationToken>, IVerificationTokenRepository
    {
        public VerificationTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByToken(string token)        
            => await _dbContext.Set<VerificationToken>().Where(x => x.Value == token).Select(x => x.User).FirstOrDefaultAsync();       
    }
}
