using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface ISubjectService
    {
        Task<long> AddStudentAsync(IAddSubjectRequest addSubjectRequest);
        Task DeleteSubjectAsync(long subjectId);
    }
}
