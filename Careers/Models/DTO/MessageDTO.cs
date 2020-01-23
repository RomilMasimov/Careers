using System.Collections.Generic;

namespace Careers.Models.DTO
{
    public class MessageDTO
    {
        public int DialogId { get; set; }
        public string ReceiverId { get; set; }
        public IEnumerable<string> ImgPaths { get; set; }
        public string Message { get; set; }
        public string AuthorImageUrl { get; set; }
    }
}
