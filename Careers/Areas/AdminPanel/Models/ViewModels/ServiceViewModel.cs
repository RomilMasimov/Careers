﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Areas.AdminPanel.Models.ViewModels
{
    public class ServiceViewModel
    {
        public string DescriptionRU { get; set; }
        public string DescriptionAZ { get; set; }
        public int SubCateoryId { get; set; }

        public SelectList Categories { get; set; }
        public SelectList SubCategories { get; set; }
    }
}
