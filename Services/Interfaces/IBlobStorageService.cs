namespace TalentoLocal.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file);
        string GenerateSasUrl(string blobName, int minutes = 20);
    }
}
