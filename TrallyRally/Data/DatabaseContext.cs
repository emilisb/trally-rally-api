using TrallyRally.Models;
using Microsoft.EntityFrameworkCore;

namespace TrallyRally.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
    }
}