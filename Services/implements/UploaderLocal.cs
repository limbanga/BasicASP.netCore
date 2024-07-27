using System.IO;

namespace ClothesStore.Services.implements
{
    public class UploaderLocal : IUploader
    {
        const string UPLOAD_FOLDER = "uploads";

        public async Task<string> UploadAsync(string folder, string fileName, IFormFile fileContent)
        {
            // Get the root directory of the project
            string rootDirectory = AppContext.BaseDirectory;
            string temp = Directory.GetParent(rootDirectory)!.FullName;
            for (int i = 0; i< 3; i++)
            {
                temp = Directory.GetParent(temp)!.FullName;
            }
            rootDirectory = Path.Combine(temp, "wwwroot");
            
            var uploadDirectory = Path.Combine(rootDirectory, UPLOAD_FOLDER, folder);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // get the file extension
            var fileExtension = Path.GetExtension(fileContent.FileName);
            if (fileExtension != null) {
                fileName += fileExtension;
            }
            // upload the file to the server
            using (var fileStream = new FileStream(Path.Combine(uploadDirectory, fileName), FileMode.Create))
            {
                await fileContent.CopyToAsync(fileStream);
            }

            return fileName;
        }

         
    }
}
