using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Configurations
{
    public sealed class StudentExamConfiguration : IEntityTypeConfiguration<StudentExam>
    {
        public void Configure(EntityTypeBuilder<StudentExam> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //builder.HasOne(x => x.Student)
            //    .WithMany();

            //builder.HasOne(x => x.Subject)
            //    .WithMany();

            //builder.HasOne<Student>()
            //    .WithMany(x => x.StudentExams)
            //    .HasForeignKey(x => x.StudentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne<Subject>()
            //    .WithMany(x => x.StudentExams)
            //    .HasForeignKey(x => x.SubjectId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
