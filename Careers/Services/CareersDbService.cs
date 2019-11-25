using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.EF;
using Careers.Models;
using Microsoft.EntityFrameworkCore;

namespace Careers.Services
{
    public class CareersDbService
    {
        private readonly CareersDbContext _context;

        public CareersDbService(CareersDbContext context)
        {
            _context = context;
        }

        public void addCategories(Dictionary<string, List<string>> dictionary)
        {




            //foreach (KeyValuePair<string, List<string>> item in dictionary)
            //{
            //    var subcategory = _context.SubCategories.FirstOrDefault(x => x.Description == item.Key);
            //    foreach (string one in item.Value)
            //    {
            //        _context.Services.Add(new Service { SubCategory = subcategory, Name = one });
            //    }
            //}

            // var cat = _context.Categories.FirstOrDefault(x => x.Description == "Врачи");

            // foreach (var one in names)
            // {
            // _context.SubCategories.Add(new SubCategory { Description = one,Category =cat});
            //}

           // _context.SaveChanges();
        }
    }
}
