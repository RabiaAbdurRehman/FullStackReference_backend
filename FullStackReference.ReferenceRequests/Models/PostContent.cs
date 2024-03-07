namespace FullStackReference.ReferenceRequests.Models
{
    public class PostContent
    {

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }


        public int UserId { get; set; } //UserID

        //For navigation properties
        public UserDataDto Newb { get; set; }
    }
}
