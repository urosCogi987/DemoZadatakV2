using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.Persistance.Configurations
{
    public sealed class VerificationTokenConfiguration : IEntityTypeConfiguration<VerificationToken>
    {
        public void Configure(EntityTypeBuilder<VerificationToken> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
