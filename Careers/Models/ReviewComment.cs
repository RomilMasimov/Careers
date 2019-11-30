namespace Careers.Models
{
    public class ReviewComment
    {
        public int Id { get; set; }
        //path to json file
        public string LogFilePath { get; set; }

        public Review Review { get; set; }
        public int ReviewId { get; set; }
    }
}
