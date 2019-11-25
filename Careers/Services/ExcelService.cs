using System.Collections.Generic;
using System.Linq;
using IronXL;

namespace Careers.Services
{
    public class ExcelService
    {



        public Dictionary<string, List<string>> GetSubCategories()
        {
            var arrs = new List<List<string>>();
            var dictionary = new Dictionary<string, List<string>>();
            var keys = new List<string>();
            List<string> list = null;
            WorkBook workbook = WorkBook.Load(@"D:\Desktop\Profi.xlsx");
            WorkSheet sheet = workbook.WorkSheets.FirstOrDefault(x => x.Name == "Врачи");

            foreach (var cell in sheet["A1:A2045"])
            {
                if (!cell.IsEmpty)
                {
                    keys.Add(cell.Text);
                    if (list != null)
                    {
                        arrs.Add(list);
                    }
                    list = new List<string>();
                }
                var key = cell.AddressString.Replace('A', 'B');
                if (!sheet[key].IsEmpty) list.Add(sheet[key].StringValue);
            }

            arrs.Add(list);


            for (int i = 0; i < keys.Count; i++)
            {
                dictionary.Add(keys[i],arrs[i]);
            }

            if (arrs.Count == keys.Count)
            {
                return dictionary;
            }

            return null;
        }











    }



}
