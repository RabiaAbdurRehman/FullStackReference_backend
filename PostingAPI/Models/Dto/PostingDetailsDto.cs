using System.ComponentModel.DataAnnotations.Schema;

namespace PostingAPI.Models.Dto
{
    public class PostingDetailsDto
    {
        public int PostingDetailsId { get; set; }
       
        public int PostingId { get; set; }
       
        public int LikeStatus { get; set; }
        public string ShareStatus { get; set; }
        public DateTime EntryDate { get; set; }
       
        //public Posting Posting { get; set; }
    }
}
