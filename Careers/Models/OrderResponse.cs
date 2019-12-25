namespace Careers.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }
       public int Text { get; set; }
        public int PriceMin { get; set; }
        public int? PriceMax { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
    }
}
