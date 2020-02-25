using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class MeasurmentViewModel
    {
        public string TextAZ { get; set; }
        public string TextRU { get; set; }

        public IEnumerable<Measurement> Measurements { get; set; }
    }
}
