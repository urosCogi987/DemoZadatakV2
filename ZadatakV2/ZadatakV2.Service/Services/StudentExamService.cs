using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class StudentExamService : IStudentExamService
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IMapper _mapper;

        public StudentExamService(IStudentExamRepository studentExamRepository, IMapper mapper)
        {
            _studentExamRepository = studentExamRepository;
            _mapper = mapper;
        }

        public async Task<long> AddStudentExamAsync(IAddStudentExamRequest addStudentExamRequest)
        {
            StudentExam studentExam = _mapper.Map<StudentExam>(addStudentExamRequest);
            studentExam.TakenOn = DateTime.UtcNow;
            return await _studentExamRepository.AddStudentExamAsync(studentExam);
        }
    }
}
