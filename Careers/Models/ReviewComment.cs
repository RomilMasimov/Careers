namespace Careers.Models
{
    public class ReviewComment
    {
        public int Id { get; set; }
        public string LogFilePath { get; set; }

        public Review Review { get; set; }
        public int ReviewId { get; set; }
    }
}
