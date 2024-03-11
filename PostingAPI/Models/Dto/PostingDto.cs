using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostingAPI.Models.Dto
{
    public class PostingDto
    {
        public int PostingId { get; set; }
        required
        public string UserId
        { get; set; }
        required
        public string PostContent
        { get; set; }
        required
        public DateTime PostingDate
        { get; set; }
        public int? DeletionFlag { get; set; }
        [NotMapped]
        public IEnumerable<PostingDetailsDto>? PostDetails { get; set; }
    }
}
