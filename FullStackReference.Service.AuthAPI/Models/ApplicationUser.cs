using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackReference.Service.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        //[NotMapped]
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
    //public class Role : IdentityRole
    //{
    //    public ICollection<UserRole> UserRoles { get; set; }
    //}
    //public class UserRole : IdentityUserRole<string>
    //{
    //    public ApplicationUser User { get; set; }
    //    public Role Role { get; set; }
    //}
}
