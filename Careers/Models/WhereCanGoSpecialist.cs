using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Models
{
    public class WhereCanGoSpecialist
    {
        public int Id { get; set; }

        public MeetingPoint WhereCanGo { get; set; }
        public int WhereCanGoId { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
    }
}
