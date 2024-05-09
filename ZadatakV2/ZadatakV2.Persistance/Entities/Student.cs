namespace ZadatakV2.Persistance.Entities
{
    public sealed class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Index { get; set; }        
        public string Email { get; set; }        

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
