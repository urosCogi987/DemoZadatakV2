using AutoMapper;
using ZadatakV2.Dto.Models;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.Dto.MappingProfiles
{
    public sealed class LoginProfile : Profile
    {
        public LoginProfile()
            => CreateMap<ILoginServiceResponse, ILoginResponse>()
                    .ConstructUsing(serviceResponse => new LoginResponse() 
                                        {
                                            AccessToken = serviceResponse.AccessToken,
                                            RefreshToken = serviceResponse.RefreshToken
                                        });    
    }
}
