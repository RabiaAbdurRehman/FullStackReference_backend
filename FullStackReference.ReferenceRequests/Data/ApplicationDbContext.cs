using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FullStackReference.ReferenceRequests.Models;

namespace FullStackReference.ReferenceRequests.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ReferenceRequest> ReferenceRequest { get; set; }
        public DbSet<UserDataDto> Users { get; set; }
        public DbSet<PostContent> PostContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReferenceRequest>().HasKey(r => r.ReferenceRequestId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
