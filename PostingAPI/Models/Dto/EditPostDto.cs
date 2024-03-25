namespace PostingAPI.Models.Dto
{
    public class EditPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostingDate { get; set; } = DateTime.Now;
    }
}
