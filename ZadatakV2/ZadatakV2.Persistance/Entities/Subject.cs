namespace ZadatakV2.Persistance.Entities
{
    public sealed class Subject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
