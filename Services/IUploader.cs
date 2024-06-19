namespace ClothesStore.Services
{
    public interface IUploader
    {
        Task<string> UploadAsync(string folder, string fileName, IFormFile fileContent);
    }
}
