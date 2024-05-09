using Microsoft.Extensions.Localization;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.Resources;

namespace ZadatakV2.Service.Services
{
    public class GradeValidation : IGradeValidation
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IGradeRepository _gradeRepository;

        public GradeValidation(IStringLocalizer<Resource> localizer, IGradeRepository gradeRepository)
        {
            _localizer = localizer;
            _gradeRepository = gradeRepository;
        }

        public async Task ValidateGrade(IAddGradeRequest addGradeRequest)
        {
            await CheckIfGradeExists(addGradeRequest.StudentId, addGradeRequest.SubjectId);

            if (addGradeRequest.Value < 6 || addGradeRequest.Value > 10)
                throw new InvalidRequestException(_localizer[Resource.GRADE_VALUES]);                        
        }

        private async Task CheckIfGradeExists(long studentId, long subjectId)
        {
            if (!await _gradeRepository.DoesGradeExist(studentId, subjectId))
                throw new InvalidRequestException(_localizer[Resource.GRADE_ALREADY_EXISTS]);
        }
    }
}
