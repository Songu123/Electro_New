using E_commerce.Repositories.Interfaces;

namespace E_commerce.Repositories.Implements
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void DeleteFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            // Xử lý cả trường hợp path có / hoặc không có /
            var relativePath = filePath.TrimStart('/');

            // Nếu path chỉ là tên file (từ code cũ), thêm images/products
            if (!relativePath.Contains("images/products"))
            {
                relativePath = $"images/products/{relativePath}";
            }

            var fullPath = Path.Combine(_env.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            // wwwroot
            var wwwRootPath = _env.WebRootPath;

            // Tạo đường dẫn đầy đủ: wwwroot/images/products
            var folderPath = Path.Combine(wwwRootPath, "images", folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // tên file unique
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // path đầy đủ tới file
            var fullPath = Path.Combine(folderPath, fileName);

            // save file
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối từ wwwroot (chỉ tên file để tương thích với code cũ)
            return fileName;
        }
    }
}
