using ZadatakV2.Shared.Interfaces;

namespace ZadatakV2.Dto.Models
{
    public sealed class AddStudentRequest : IAddStudentRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Index { get; set; }
        public string Email { get; set; }
    }
}
