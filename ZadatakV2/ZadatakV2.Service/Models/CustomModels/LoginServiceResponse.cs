using ZadatakV2.Shared.NewFolder;

namespace ZadatakV2.Service.Models.CustomModels
{
    public class LoginServiceResponse : ILoginServiceResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
