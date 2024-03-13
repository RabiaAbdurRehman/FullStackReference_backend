using FullStackReference.Service.AuthAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStackReference.Service.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,Role,String,IdentityUserClaim<string>,
        UserRole,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

       // public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }
        //public DbSet<Role> Role { get; set; }
        //public IEnumerable<object> AspNetUsers { get; internal set; }
        //public IEnumerable<object> AspNetUserRoles { get; internal set; }
        //public IEnumerable<object> AspNetRoles { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>(x =>
            //{
            //    x.HasMany(e => e.UserRoles)
            //    .WithOne(e => e.ApplicationUser)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
            //});
            //modelBuilder.Entity<Role>(x =>
            //{
            //    x.HasMany(e => e.UserRoles)
            //    .WithOne(e => e.Role)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired();
            //});

        }
    }
}
