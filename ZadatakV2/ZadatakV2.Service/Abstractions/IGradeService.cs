using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface IGradeService
    {
        Task<long> AddGradeAsync(IAddGradeRequest addGradeRequest);
    }
}
