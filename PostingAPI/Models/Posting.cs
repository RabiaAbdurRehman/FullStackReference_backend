using PostingAPI.Data;
using PostingAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostingAPI.Models
{
    public class Posting
    {
        [Key]
        public int PostingId { get; set; }
        required
        public string UserId { get; set; }
        required
        public string PostContent { get; set; }
        required
        public DateTime PostingDate { get; set; }
        public int ?DeletionFlag  { get; set; }
        [NotMapped]
        public IEnumerable<PostingDetails>? PostDetails { get; set; }
        [NotMapped]
        public virtual IEnumerable<UserAuthDto>? userdto { get; set; }
        //public List<PostingDetails> PostingDetails { get; set; }

    }
}
