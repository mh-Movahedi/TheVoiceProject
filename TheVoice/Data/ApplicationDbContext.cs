using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheVoice.Models;

namespace TheVoice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Mentor> Mentores { get; set; }
        public DbSet<Candicate> Candicates { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Score> Scores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}