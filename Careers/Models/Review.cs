using System;

namespace Careers.Models
{
    public class Review
    {
        public int SpecialistServiceId { get; set; }
        public int SpecialistId { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }



    }
}