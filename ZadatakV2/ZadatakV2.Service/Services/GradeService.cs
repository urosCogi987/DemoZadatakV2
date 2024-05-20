using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.Resources;

namespace ZadatakV2.Service.Services
{
    public class GradeService(IGradeRepository gradeRepository, IMapper mapper) : IGradeService
    {       
        public async Task AddGradeAsync(IAddGradeRequest addGradeRequest)
        {
            await ValidateGrade(addGradeRequest);
            Grade grade = mapper.Map<Grade>(addGradeRequest);            
            await gradeRepository.AddItemAsync(grade);
        }

        private async Task ValidateGrade(IAddGradeRequest addGradeRequest)
        {
            if (await gradeRepository.DoesGradeExist(addGradeRequest.StudentId, addGradeRequest.SubjectId))                
                throw new InvalidRequestException(Resource.GRADE_ALREADY_EXISTS);

            if (addGradeRequest.Value < 6 || addGradeRequest.Value > 10)                
                throw new InvalidRequestException(Resource.GRADE_VALUES);
        }
    }
}
