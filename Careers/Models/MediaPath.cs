using System.Collections.Generic;

namespace Careers.Models
{
    public class MediaPath
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public IEnumerable<MessageMediaPath> MessageMediaPaths { get; set; }

    }
}