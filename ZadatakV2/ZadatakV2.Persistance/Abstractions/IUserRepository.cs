using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {        
        Task<User?> FindUserByEmailAsync(string email);                
        Task<bool> IsEmailUniqueAsync(string index);
    }
}
