using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public sealed class AddGradeRequest : IAddGradeRequest
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Value { get; set; }        
    }
}
