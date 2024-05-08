using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZadatakV2.Persistance.Entities;

namespace ZadatakV2.WebApi.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {       
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();            

            builder.HasIndex(x => x.Email).IsUnique();            
        }
    }
}
