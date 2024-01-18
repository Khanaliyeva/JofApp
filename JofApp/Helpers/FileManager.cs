namespace JofApp.Helpers
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file, string envPath, string folderpath) 
        {
            string fileName=file.FileName;
            if (!Directory.Exists(envPath + folderpath))
            {
                Directory.CreateDirectory(envPath + folderpath);
            }
            string filePath=envPath + folderpath+fileName;
            using(FileStream fileStream=new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;

        }

        public static void DeleteFile(this string ImgUrl, string envPath, string folderPath)
        {
            string filePath=envPath + folderPath+ImgUrl;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
