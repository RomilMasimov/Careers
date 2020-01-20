namespace Careers.ViewModels.Partial
{
    public class ChatWithSpecViewModel
    {
        public int OrderId { get; set; }
        public int SpecialistId { get; set; }

        public ChatWithSpecViewModel(int orderid,int specid)
        {
            OrderId = orderid;
            SpecialistId = specid;
        }
    }
}
