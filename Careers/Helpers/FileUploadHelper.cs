using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Careers.Models;
using Careers.Models.Enums;

namespace Careers.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> UploadAsync(IFormFile file, ImageOwnerEnum type)
        {
            if (file == null) throw new Exception("File was not uploaded!");

            const string specialistProfile = "media/specialistProfile";
            const string clientProfile = "media/clientProfile";
            var selectedPath = "";

            switch (type)
            {
                case ImageOwnerEnum.Client:
                    selectedPath = clientProfile;
                    break;

                case ImageOwnerEnum.Specialist:
                    selectedPath = specialistProfile;
                    break;

                default: throw new Exception("File was not uploaded!");
            }

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            await using var fs = new FileStream($"wwwroot/{selectedPath}/{filename}", FileMode.Create);
            await file.CopyToAsync(fs);
            return $"{selectedPath}/{filename}";
        }

        public static bool Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;

            var localPath = $"wwwroot/{path}";
            if (!File.Exists(localPath)) return false;
            File.Delete(localPath);
            return true;
        }
    }
}