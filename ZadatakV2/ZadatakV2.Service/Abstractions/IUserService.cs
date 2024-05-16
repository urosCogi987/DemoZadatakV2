namespace ZadatakV2.Service.Abstractions
{
    public interface IUserService
    {
        Task BlockUserAsync(long id);
    }
}
