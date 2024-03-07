using System.ComponentModel.DataAnnotations;

namespace FullStackReference.Service.PostingAPI.Modal
{
    public class Posting
    {
        [Key]
        public int PostingId { get; set; }
        [Required]
        public string UserId { get; set; }
        public string PostingContent { get; set; }

    }
}
