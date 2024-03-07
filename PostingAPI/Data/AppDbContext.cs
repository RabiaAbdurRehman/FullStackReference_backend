using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostingAPI.Models;
using PostingAPI.Models.Dto;
using System.Reflection.Metadata;

namespace PostingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Posting> Posting { get; set; }
        public DbSet<PostingDetails> PostingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new PostingConfig());
            //modelBuilder.ApplyConfiguration(new PostingDetailsConfig());

            // modelBuilder.Entity<Posting>()
            //.HasMany(e => e.PostDetails)
            //.WithOne(e => e.Posting)
            //.HasForeignKey(e => e.PostingId)
            //.IsRequired(false);

            //modelBuilder.Entity<PostingDetails>()
            //    .HasOne(d => d.PostinguserIdentity)
            //    .WithMany(c => c.Comments)
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired();

            modelBuilder.Entity<PostingDetails>()
                .HasOne(x=>x.Posting)
                .WithMany(y => y.PostDetails)
                .HasForeignKey(z=>z.PostingId)
                .IsRequired();

        }

    }
}
