using System.ComponentModel.DataAnnotations;

namespace PostingAPI.Models.Dto
{
    public class UserAuthDto
    {
        [Key]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
