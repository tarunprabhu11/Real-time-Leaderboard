using Microsoft.EntityFrameworkCore;
using LeaderBoard.Model;

namespace LeaderBoard.Data
{ 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
