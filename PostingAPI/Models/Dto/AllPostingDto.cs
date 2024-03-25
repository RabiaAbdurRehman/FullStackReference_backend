namespace PostingAPI.Models.Dto
{
    public class AllPostingDto
    {
        public PostingDto posting { get; set; }
       
        public IEnumerable<PostingDetailsDto>? details { get; set; }
    }
}
