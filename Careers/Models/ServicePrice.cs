namespace Careers.Models
{
    public class ServicePrice
    {
        public int  Id { get; set; }
        public int Price { get; set; }

        public int  ServiceId { get; set; }
        public Service Service { get; set; }
    }
}