using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class StudentExamService(IStudentExamRepository studentExamRepository, IMapper mapper) : IStudentExamService
    {        
        public async Task AddStudentExamAsync(IAddStudentExamRequest addStudentExamRequest)
        {
            StudentExam studentExam = mapper.Map<StudentExam>(addStudentExamRequest);
            studentExam.TakenOn = DateTime.UtcNow;
            await studentExamRepository.AddItemAsync(studentExam);
        }
    }
}
