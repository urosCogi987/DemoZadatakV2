using AutoMapper;
using ZadatakV2.Dto.Models;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Dto.MappingProfiles
{
    public class StudentExamProfile : Profile
    {
        public StudentExamProfile()
            => CreateMap<AddStudentExamRequest, StudentExam>();
    }
}
