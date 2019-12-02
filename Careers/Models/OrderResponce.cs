namespace Careers.Models
{
    public class OrderResponce
    {
        public int Id { get; set; }
       public int Text { get; set; }
        public int? PriceMax { get; set; }
        public int PriceMin { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Specialist Specialis { get; set; }
        public int SpecialisId { get; set; }
    }
}
