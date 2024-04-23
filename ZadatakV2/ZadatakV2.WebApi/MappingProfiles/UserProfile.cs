using AutoMapper;
using ZadatakV2.WebApi.Entities;
using ZadatakV2.WebApi.Models;

namespace ZadatakV2.WebApi.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()        
            => CreateMap<RegisterUserRequest, User>();
        
    }
}
