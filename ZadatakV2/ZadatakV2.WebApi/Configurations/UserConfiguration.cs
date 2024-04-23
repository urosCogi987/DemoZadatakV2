using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZadatakV2.WebApi.Entities;

namespace ZadatakV2.WebApi.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
       
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasData(new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "admin@email.com",
                    Password = "LcqvHMJDQsudN1qg6VRipw==;UUWrb5u0pKLH0MpmvMVfFCybh7IK0XXv+PV/jjjJLv0="
                }
            });
        }
    }
}
