using MediatrExample.Core.Entities;
using MediatrExample.Data.Configuration;
using MediatrExample.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        #region Database Tables Set

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserSeed());
            base.OnModelCreating(builder); 
        }
    }
}
