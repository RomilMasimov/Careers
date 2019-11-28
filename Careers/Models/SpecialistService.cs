﻿namespace Careers.Models
{
    public class SpecialistService
    {
        public int Id { get; set; }
        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
        public Specialist Specialist { get; set; }
        public int SpecialistId { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public Measure Measure { get; set; }
        public int MeasureId { get; set; }

    }
}