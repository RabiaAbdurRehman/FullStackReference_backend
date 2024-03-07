namespace PostingAPI.Models.Dto
{
    public class PostInsertDto
    {
        public int ?PostingId { get; set; }
        public string UserId { get; set; }
        public string PostContent { get; set; }
    
        public DateTime PostingDate { get; set; }
       
    }
}
