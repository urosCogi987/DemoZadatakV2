namespace ZadatakV2.Persistance.Entities
{
    public sealed class StudentExam
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Mark { get; set; }
        public DateTime TakenOn { get; set; }

        public Student? Student { get; set; }
        public Subject? Subject { get; set; }
    }
}
