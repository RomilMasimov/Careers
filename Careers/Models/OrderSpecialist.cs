namespace Careers.Models
{
    public class OrderSpecialist
    {
        public int  Id { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
