using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public class AddStudentExamRequest : IAddStudentExamRequest
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Mark { get; set; }
    }
}
