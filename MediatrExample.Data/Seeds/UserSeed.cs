using MediatrExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatrExample.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = 1,
                FirstName = "Hasan",
                LastName = "Erdal",
                Gsm = "5555555555",
                Mail = "test@test.com",
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            });
        }
    }
}
