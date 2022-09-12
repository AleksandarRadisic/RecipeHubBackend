using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RecipeHub.ClassLib.Utilities
{
    public class PictureUtility
    {
        public static string savePicture(string destination, IFormFile file)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), destination));
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), destination, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public static void deletePicture(string destination, string fileName)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), destination));
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), destination, fileName));
        }

        public static string convertToBase64(string destination, string fileName)
        {
            try
            {
                byte[] data = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), destination, fileName));
                return Convert.ToBase64String(data);
            }
            catch
            {
                return "";
            }
        }
    }
}
