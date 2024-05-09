using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;
        private readonly IGradeValidation _gradeValidation;

        public GradeService(IGradeRepository gradeRepository,
                            IMapper mapper,
                            IGradeValidation gradeValidation)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
            _gradeValidation = gradeValidation;
        }

        public async Task AddGradeAsync(IAddGradeRequest addGradeRequest)
        {
            await _gradeValidation.ValidateGrade(addGradeRequest);
            Grade grade = _mapper.Map<Grade>(addGradeRequest);            
            await _gradeRepository.AddGradeAsync(grade);
        }
    }
}
