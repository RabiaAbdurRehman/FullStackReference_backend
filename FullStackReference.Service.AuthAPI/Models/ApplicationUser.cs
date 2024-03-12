using Microsoft.AspNetCore.Identity;

namespace FullStackReference.Service.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
       public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    public class UserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Role Role { get; set; }
    }
}
