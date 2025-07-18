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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasKey(u => u.UserId);

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.RefreshToken)
            //    .WithOne(rt => rt.User)
            //    .HasForeignKey<RefreshToken>(rt => rt.UserId);

            //modelBuilder.Entity<RefreshToken>()
            //    .HasKey(rt => rt.Id);

            //modelBuilder.Entity<RefreshToken>()
            //    .HasIndex(rt => rt.Token)
            //    .IsUnique();

            //modelBuilder.Entity<Role>()
            //    .HasKey(r => r.RoleId);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Roles)
            //    .WithMany(r => r.Users)
            //    .UsingEntity(j => j.ToTable("UserRoles"));

        }
    }
}
