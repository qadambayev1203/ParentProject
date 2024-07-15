using Entities.Model.AnyClasses;

namespace TSTUWebAPI.Controllers.FileControllers
{
    public class FileUploadRepository
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".mp4", ".avi", ".png", ".gif", ".doc", "docx", ".xlsx", ".docx", ".pdf", ".ppt", ".pptx" };
        private readonly string[] allowedExtensionsImg = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] allowedExtensionsDoc = { ".doc", ".docx", ".pdf", ".ppt", ".pptx", ".xlsx" };
        private readonly string[] allowedExtensionsVideo = { ".mp4", ".avi" };


        public string SaveFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return (null);

                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    return ("Invalid file extension!");

                var filePathStr = @"file-uploads\any";

                if (allowedExtensionsImg.Contains(fileExtension))
                {
                    filePathStr = @"file-uploads\images";
                }
                else if (allowedExtensionsDoc.Contains(fileExtension))
                {
                    filePathStr = @"file-uploads\documents";
                }
                else if (allowedExtensionsVideo.Contains(fileExtension))
                {
                    filePathStr = @"file-uploads\videos";
                }
                var filePath = Path.Combine(filePathStr, Guid.NewGuid().ToString() + fileExtension);

                using (var stream = new FileStream( filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return GetNewFilePath(SessionClass.fileUploadsUrl + filePath); ;
            }
            catch
            {
                return "Error!";
            }
        }

        public bool DeleteFileAsync(string Url)
        {
            try
            {
                string filePath = SessionClass.fileUploadsUrl + Url;
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public string GetNewFilePath(string filePath)
        {
            if (filePath == null)
            {
                return null;
            }

            string newPath = filePath.Replace(@"\\", "/");

            newPath = newPath.Replace(@"\", "/");

            return newPath;
        }
    }
}
