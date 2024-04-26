using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }            
        
        public async Task<long> AddStudentAsync(IAddStudentRequest addStudentRequest)
        {
            Student student = _mapper.Map<Student>(addStudentRequest);
            return await _studentRepository.AddStudentAsync(student);
        }
    }
}
