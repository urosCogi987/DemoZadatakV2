namespace ZadatakV2.Persistance.Entities
{
    public sealed class Grade
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Value { get; set; }
    }
}
