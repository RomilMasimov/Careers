using System;
using System.Collections.Generic;

namespace Careers.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public IEnumerable<ReviewMedia> ImagePathes { get; set; }
    }
}