using PostingAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostingAPI.Models.Dto
{
    public class PostingDetails
    {
        [Key]
        public int PostingDetailsId { get; set; }
        [ForeignKey("PostingId")]
        public int PostingId { get; set; }

        public int? LikeStatus { get; set; }
        public string? ShareStatus { get; set; }
        public DateTime EntryDate { get; set; }
        public String UserId { get; set; }
        public String? Comments { get; set; }
        public Posting? Posting { get; set; }
        // public PostingUser? PostinguserIdentity { get; set; }
        [NotMapped]
        public virtual IEnumerable<UserAuthDto>? userdtoDetails { get; set; }



    }
}
