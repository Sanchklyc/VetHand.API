using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results;
using System.Text.RegularExpressions;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {

        public static string Add(IFormFile file,string folder="")
        {
            if (file.Length > 0)
            {
                string tempPath = Path.GetTempFileName();
                using (FileStream fs = new FileStream(tempPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                string newPath = NewPath(file);
                File.Move(tempPath, Environment.CurrentDirectory + @"\wwwroot\images\"+ folder + newPath);
                return newPath;
            }
            return null;
        }

        public static string AddFromBase64(string base64, string folder = "")
        {
            base64 = Regex.Replace(base64, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            byte[] imageBytes = Convert.FromBase64String(base64);
            string newPath = NewPathForBase64();
            File.WriteAllBytes(Environment.CurrentDirectory + @"\wwwroot\images\" + folder + @"\" + newPath, imageBytes);
            return newPath;
        }

        public static IResult Remove(string path,string folder="")
        {
            path = Environment.CurrentDirectory + @"\wwwroot\images\" + folder + path;
            if (!File.Exists(path))
            {
                return new ErrorResult("File not found.");
            }
            File.Delete(path);
            return new SuccessResult("File deleted succesfully.");
        }

        public static string Update(string oldPath, IFormFile file)
        {
            Remove(oldPath);
            return Add(file);
        }

        public static string NewPath(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;
            return Guid.NewGuid() + fileExtension;
        }
        public static string NewPathForBase64()
        {
            return Guid.NewGuid().ToString("N") + ".jpg";
        }
    }
}
