
using System.Xml.Linq;

namespace BookLibrary.Services
{
    public class FileUploadService
    {
        public async Task<string> UploadFile(IFormFile file, string Name)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    

                    using (var fileStream = new FileStream(Path.Combine(path, Name + file.FileName.Substring(file.FileName.IndexOf("."))), FileMode.Create))
                    {
                        
                        await file.CopyToAsync(fileStream);
                    }
                    return Path.Combine(Path.Combine("UploadedFiles/", Name+file.FileName.Substring(file.FileName.IndexOf("."))));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    
    }
}
