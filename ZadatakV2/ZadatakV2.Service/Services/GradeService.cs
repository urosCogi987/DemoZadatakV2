using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;
using ZadatakV2.Shared.Resources;

namespace ZadatakV2.Service.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;        

        public GradeService(IGradeRepository gradeRepository,
                            IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;            
        }

        public async Task AddGradeAsync(IAddGradeRequest addGradeRequest)
        {
            await ValidateGrade(addGradeRequest);
            Grade grade = _mapper.Map<Grade>(addGradeRequest);            
            await _gradeRepository.AddGradeAsync(grade);
        }

        private async Task ValidateGrade(IAddGradeRequest addGradeRequest)
        {
            if (await _gradeRepository.DoesGradeExist(addGradeRequest.StudentId, addGradeRequest.SubjectId))                
                throw new InvalidRequestException(Resource.GRADE_ALREADY_EXISTS);

            if (addGradeRequest.Value < 6 || addGradeRequest.Value > 10)                
                throw new InvalidRequestException(Resource.GRADE_VALUES);
        }
    }
}
