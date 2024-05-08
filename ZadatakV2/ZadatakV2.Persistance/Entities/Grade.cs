namespace ZadatakV2.Persistance.Entities
{
    public sealed class Grade
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Value { get; set; }
        public DateTime AddedOn { get; set; }

        public User User { get; set; }
        public Subject Subject { get; set; }
    }
}
