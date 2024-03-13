using Microsoft.AspNetCore.Identity;
using PostingAPI.Models.Dto;
using System.Xml.Linq;

namespace PostingAPI.Data
{
    public class PostingUser : IdentityUser
    {
        public ICollection<PostingDetails> Comments { get; set; }
    }
}

// Just For test Commit
