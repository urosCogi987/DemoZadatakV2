using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class StudentService(IStudentRepository studentRepository, IMapper mapper) : IStudentService
    {                   
        public async Task AddStudentAsync(IAddStudentRequest addStudentRequest)
        {            
            if (!await studentRepository.IsIndexUniqueAsync(addStudentRequest.Index))
                throw new UniqueConstraintViolationException($"Student with index: {addStudentRequest.Index} already exists.");

            Student student = mapper.Map<Student>(addStudentRequest);
            await studentRepository.AddItemAsync(student);
        }
    }
}
