using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.Repositories
{
    public class MediaRepository
    {
        public async Task<string> AddAsync(string root, Stream file)
        {
            var filename = $"{Guid.NewGuid()}.png";
            using var fs = new FileStream(@$"{root}/{filename}", FileMode.Create);
            await file.CopyToAsync(fs);
            return filename;
        }

        public void Delete(string root, string filename)
        {
            var path = @$"{root}\{filename}";
            if (!File.Exists(path)) return;
            File.Delete(path);
        }
    }
}
