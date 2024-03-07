using FullStackReference.Service.PostingAPI.Modal;
using Microsoft.EntityFrameworkCore;

namespace FullStackReference.Service.PostingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Posting> Posting { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
