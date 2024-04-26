using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface IStudentService
    {
        Task<long> AddStudentAsync(IAddStudentRequest addStudentRequest);
    }
}
