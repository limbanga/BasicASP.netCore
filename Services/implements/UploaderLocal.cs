
namespace ClothesStore.Services.implements
{
    public class UploaderLocal : IUploader
    {
        const string UPLOAD_FOLDER = "uploads";

        public async Task<string> UploadAsync(string folder, string fileName, IFormFile fileContent)
        {
            string rootDirectory = AppContext.BaseDirectory;
            string temp = System.IO.Directory.GetParent(rootDirectory).FullName;
            for (int i = 0; i< 3; i++)
            {
                temp = System.IO.Directory.GetParent(temp).FullName;
            }
            rootDirectory = Path.Combine(temp, "wwwroot");
            
            var uploadDirectory = Path.Combine(rootDirectory, UPLOAD_FOLDER, folder);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            using(var fileStream = new FileStream(Path.Combine(uploadDirectory, fileName), FileMode.Create))
            {
                await fileContent.CopyToAsync(fileStream);
            }
            return fileName;
        }

         
    }
}
