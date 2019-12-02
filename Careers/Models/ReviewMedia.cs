namespace Careers.Models
{
    public class ReviewMedia
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public Review Review { get; set; }
        public int ReviewId { get; set; }

    }
}
