using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Configurations
{
    public sealed class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(x => new { x.SubjectId, x.StudentId});                       
        }
    }
}
