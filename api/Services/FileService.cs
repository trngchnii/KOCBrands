namespace api.Services
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file,string uploadFolder);
        Task<bool> DeleteFileAsync(string filePath);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file,string uploadFolder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Đảm bảo thư mục Uploads tồn tại trong thư mục gốc của project API
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(),"Uploads",uploadFolder);

            // Kiểm tra và tạo thư mục nếu chưa có
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Đặt tên file và đường dẫn đầy đủ
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadPath,fileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath,FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối
            return Path.Combine("Uploads",uploadFolder,fileName);
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            // Đảm bảo filePath là đường dẫn tuyệt đối
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(),"Uploads",filePath.Replace("api\\",""));

            if (File.Exists(fullPath))
            {
                // Nếu file tồn tại, xóa nó
                File.Delete(fullPath);
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }


    }
}
