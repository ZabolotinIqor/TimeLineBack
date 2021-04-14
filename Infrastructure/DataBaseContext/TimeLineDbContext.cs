using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBaseContext
{
    public class TimeLineDbContext:DbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public TimeLineDbContext(DbContextOptions<TimeLineDbContext> options) : base(options) { }
        public void InsertNew(RefreshToken token)
        {
            var tokenModel = this.RefreshTokens.SingleOrDefault(i => i.UserId == token.UserId);
            if (tokenModel != null)
            {
                this.RefreshTokens.Remove(tokenModel);
                SaveChanges();
            }
            this.RefreshTokens.Add(token);
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