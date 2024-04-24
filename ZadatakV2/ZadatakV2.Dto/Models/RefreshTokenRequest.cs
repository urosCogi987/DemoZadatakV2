namespace ZadatakV2.Dto.Models
{
    public class RefreshTokenRequest 
    {
        public string RefreshToken { get; set; }
    }
    //public class RefreshTokenRequest : IRefreshTokenRequest
    //{
    //    public string RefreshToken { get; set; }
    //}


    //public interface IRefreshTokenRequest
    //{
    //    string RefreshToken { get; }
    //}

    //public class sale
    //{

    //    public void metoda1()
    //    {
    //        var a = new RefreshTokenRequest();
    //        a.RefreshToken = "sada";
    //        this.metoda2(a);
    //    }


    //    public void metoda2(IRefreshTokenRequest refreshTokenRequest)
    //    {
    //        refreshTokenRequest.RefreshToken = "dsadada";
    //    }
    //}
}
