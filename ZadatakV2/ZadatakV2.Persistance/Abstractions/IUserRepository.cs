using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<long> AddUserAsync(User user);
        Task<User?> FindUserByEmailAsync(string email);
        Task<User?> FindUserByIdAsync(long id);
        Task UpdateUserAsync(User user);
    }
}
