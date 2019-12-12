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
            using var fileStream = file as FileStream;
            if (fileStream == null)
                return string.Empty;

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(fileStream.Name)}";
            using var fs = new FileStream(@$"{root}/{filename}", FileMode.Create);
            await fileStream.CopyToAsync(fs);
            return filename;
        }

        public void Delete(string root, string filename)
        {
            var path = root + filename;
            if (!File.Exists(path)) return;
            File.Delete(path);
        }
    }
}
