using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Careers.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> UploadAsync(IFormFile file)
        {
            string path = @"wwwroot/media/profileimages";
            if (file != null)
            {
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var r = Directory.Exists(path);
                await using var fs = new FileStream($"{path}/{filename}", FileMode.Create);
                await file.CopyToAsync(fs);
                return $"/media/profileimages/{filename}";
            }
            throw new Exception("File was not uploaded!");
        }
    }
}
