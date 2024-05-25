using HanDesign.System.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HanDesign.System.Infrastructure
{
    public class ISystemContext(DbContextOptions<ISystemContext> dbContext):DbContext(dbContext)
    {

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("User", "dbo");
                b.HasKey(a => a.Id);
            });
        }
    }
}
