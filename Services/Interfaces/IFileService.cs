namespace E_commerce.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string folderName);
        void DeleteFile(string? filePath);
    }
}
