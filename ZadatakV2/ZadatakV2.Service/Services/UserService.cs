using ZadatakV2.Domain.Repositories;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;

namespace ZadatakV2.Service.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {     
        public async Task BlockUserAsync(long id)
        {
            User? user = await userRepository.GetItemByIdAsync(id);
            if (user == null) 
                throw new EntityNotFoundException("User with that id does not exist.");

            user.IsBlocked = true;
            await userRepository.UpdateItemAsync(user);
        }
    }
}
