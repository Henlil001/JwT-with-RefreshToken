using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwT_with_RefreshToken.DataAcces
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUser",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RolesRoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UsersUserId")
                );
        }
    }
}
