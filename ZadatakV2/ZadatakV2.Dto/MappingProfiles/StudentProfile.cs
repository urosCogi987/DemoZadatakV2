using AutoMapper;
using ZadatakV2.Dto.Models;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Dto.MappingProfiles
{
    public sealed class StudentProfile : Profile
    {
        public StudentProfile()
            => CreateMap<AddStudentRequest, Student>();
    }
}
