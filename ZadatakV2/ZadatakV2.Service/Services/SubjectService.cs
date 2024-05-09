using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<long> AddStudentAsync(IAddSubjectRequest addSubjectRequest)
        {
            if (!await _subjectRepository.IsNameUniqueAsync(addSubjectRequest.Name)) 
                throw new UniqueConstraintViolationException($"Subject with name: {addSubjectRequest.Name} already exists.");

            Subject subject = _mapper.Map<Subject>(addSubjectRequest);
            return await _subjectRepository.AddSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(long subjectId)
        {
            Subject? subject = await _subjectRepository.GetSubjectByIdAsync(subjectId);
            if (subject is null)
                throw new EntityNotFoundException($"Subject with id {subjectId} does not exist.");

            await _subjectRepository.DeleteSubjectAsync(subject);
        }
    }
}
