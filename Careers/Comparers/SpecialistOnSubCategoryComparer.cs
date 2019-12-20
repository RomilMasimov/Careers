using System.Collections.Generic;
using System.Linq;
using Careers.Models;

namespace Careers.Comparers
{
    public class SpecialistOnSubCategoryComparer:IEqualityComparer<Specialist>
    {
        public bool Equals(Specialist x, Specialist y)
        {
            return x.Orders.FirstOrDefault().Service.SubCategoryId == y.Orders.FirstOrDefault().Service.SubCategoryId;
        }

        public int GetHashCode(Specialist obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
