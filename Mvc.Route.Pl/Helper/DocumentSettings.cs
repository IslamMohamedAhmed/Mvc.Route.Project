namespace Mvc.Route.Pl.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files", folderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            //sending file to database

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName,string folderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files", folderName, fileName);

            if (File.Exists(FilePath)) { File.Delete(FilePath); }

        }
    }
}
