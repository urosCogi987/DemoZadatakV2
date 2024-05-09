using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public sealed class AddSubjectRequest : IAddSubjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
