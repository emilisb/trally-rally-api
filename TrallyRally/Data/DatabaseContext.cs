using TrallyRally.Models;
using Microsoft.EntityFrameworkCore;

namespace TrallyRally.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Player>()
                .HasMany(p => p.Games)
                .WithMany(p => p.Players)
                .UsingEntity(j => j.ToTable("GamePlayer"));
        }

    }
}