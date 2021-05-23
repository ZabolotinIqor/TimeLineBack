using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBaseContext
{
    public class TimeLineDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<ApplicationTask> ApplicationTasks { get; set; }
        public TimeLineDbContext(DbContextOptions<TimeLineDbContext> options) : base(options) { }
        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshTokens.SingleOrDefault(i => i.UserId == token.UserId);
            if (tokenModel != null)
            {
                RefreshTokens.Remove(tokenModel);
                SaveChanges();
            }
            RefreshTokens.Add(token);
            SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.UserId);
            modelBuilder.Entity<RefreshToken>()
                .HasAlternateKey(c => c.Token);
            base.OnModelCreating(modelBuilder);
        }
    }
}