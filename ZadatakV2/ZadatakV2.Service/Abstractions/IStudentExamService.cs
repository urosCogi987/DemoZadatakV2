using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface IStudentExamService
    {
        Task<long> AddStudentExamAsync(IAddStudentExamRequest addStudentExamRequest);
    }
}
