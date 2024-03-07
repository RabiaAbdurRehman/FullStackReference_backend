using System.ComponentModel.DataAnnotations;

namespace FullStackReference.Service.PostingAPI.Modal.Dto
{
    public class PostingDto
    {
       public int PostingId { get; set; }
       public string UserId { get; set; }
       public string PostingContent { get; set; }
    }
}
