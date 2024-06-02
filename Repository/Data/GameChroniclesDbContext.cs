using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class GameChroniclesDbContext : DbContext
    {
        public GameChroniclesDbContext(DbContextOptions<GameChroniclesDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGame>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            modelBuilder.Entity<UserGame>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.SavedGames)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGame>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.SavedByUsers)
                .HasForeignKey(ug => ug.GameId);
        }
    }
}
