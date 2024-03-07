namespace FullStackReference.ReferenceRequests.Models
{
    public class ReferenceRequest
    {

        public int Id { get; set; }


        public int NewBId { get; set; } //UserID


        public int MentorId { get; set; } //UserID


        public int PostContentId { get; set; }

        public DateTime DateRequested { get; set; }
        public DateTime DateSubmitted { get; set; }

        public string Status { get; set; }

        //For navigation properties
        public UserDataDto Newb { get; set; }
        public PostContent PostContent { get; set; }
    }
}
