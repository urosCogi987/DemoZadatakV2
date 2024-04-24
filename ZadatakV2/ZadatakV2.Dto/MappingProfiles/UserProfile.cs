using AutoMapper;
using ZadatakV2.Dto.Models;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.WebApi.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()        
            => CreateMap<RegisterRequest, User>();
        
    }
}
