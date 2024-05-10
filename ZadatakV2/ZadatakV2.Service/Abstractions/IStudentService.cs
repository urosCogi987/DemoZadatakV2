using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface IStudentService
    {
        Task AddStudentAsync(IAddStudentRequest addStudentRequest);
    }
}
