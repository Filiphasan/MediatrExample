using MediatrExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatrExample.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.Property(x => x.FirstName).HasColumnName("FIRST_NAME").IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).HasColumnName("LAST_NAME").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Mail).HasColumnName("MAIL").HasMaxLength(150);
            builder.Property(x => x.Gsm).HasColumnName("GSM").HasMaxLength(20);
            builder.Property(x => x.PasswordHash).HasColumnName("PW_HASH").HasMaxLength(250).IsRequired();
            builder.ToTable("USERS");
        }
    }
}
