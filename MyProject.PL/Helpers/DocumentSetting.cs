using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace MyProject.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UpLoadFile(IFormFile file, string FolderName)
        {
            //string FolderPath = $"C:\\Users\\xxx68\\source\\repos\\MyProject\\MyProject.PL\\wwwroot\\Files\\images\\{FolderName}"; 
            //string FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            

            string filename= $"{Guid.NewGuid()} {Path.GetExtension(file.FileName)}";

            string filePath = Path.Combine(FolderPath, filename);

             using var FileStream= new FileStream(filePath , FileMode.Create);

            file.CopyTo(FileStream);

            return filename;
        }

        public static void DeleteFile(string FileName , string FolderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FileName, FolderName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }



    }
}
