using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanDesign.Authorization.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HanDesign.Authorization.Infrastructure
{
    public class IAuthorizationContext(DbContextOptions<IAuthorizationContext> options) : DbContext(options)
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
