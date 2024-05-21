using System.Data;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Abstractions
{
    public interface IVerificationTokenRepository : IRepository<VerificationToken>
    {
        Task<User?> GetUserByToken(string token);        
    }
}
