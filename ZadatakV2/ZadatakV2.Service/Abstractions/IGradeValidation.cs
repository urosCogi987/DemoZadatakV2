using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Abstractions
{
    public interface IGradeValidation 
    {
        Task ValidateGrade(IAddGradeRequest addGradeRequest);
    }
}
