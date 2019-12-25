namespace Careers.Models
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }

        public Filter(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Filter(int id, string name,bool selected)
        {
            Id = id;
            Name = name;
            Selected = selected;
        }
    }
}
