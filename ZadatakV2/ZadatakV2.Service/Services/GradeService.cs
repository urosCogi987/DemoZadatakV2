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

        public GradeService(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<long> AddGradeAsync(IAddGradeRequest addGradeRequest)
        {
            Grade grade = _mapper.Map<Grade>(addGradeRequest);
            grade.AddedOn = DateTime.UtcNow;
            return await _gradeRepository.AddGradeAsync(grade);
        }
    }
}
