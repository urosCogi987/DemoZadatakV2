using AutoMapper;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.Service.Abstractions;
using ZadatakV2.Shared.Exceptions;
using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Service.Services
{
    public class SubjectService(ISubjectRepository subjectRepository, IMapper mapper) : ISubjectService
    {        
        public async Task AddSubjectAsync(IAddSubjectRequest addSubjectRequest)
        {
            if (!await subjectRepository.IsNameUniqueAsync(addSubjectRequest.Name)) 
                throw new UniqueConstraintViolationException($"Subject with name: {addSubjectRequest.Name} already exists.");

            Subject subject = mapper.Map<Subject>(addSubjectRequest);
            await subjectRepository.AddItemAsync(subject);
        }

        public async Task DeleteSubjectAsync(long subjectId)
        {
            Subject? subject = await subjectRepository.GetItemByIdAsync(subjectId);
            if (subject is null)
                throw new EntityNotFoundException($"Subject with id {subjectId} does not exist.");

            await subjectRepository.DeleteItemAsync(subject);
        }
    }
}
