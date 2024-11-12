using api.Data;
using api.DTOs;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;
        public BrandRepository(ApplicationDbContext context,IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(i => i.BrandId == id);
            if (brand == null)
                return null;
            return brand;
        }

        public async Task UpdateBrandAsync(int brandId,UpdateBrandUserRequestDto requestDto)
        {
            var brand = await _context.Brands
                .Include(i => i.User) // Include User để cập nhật Avatar
                .FirstOrDefaultAsync(i => i.BrandId == brandId);

            if (brand == null)
            {
                throw new Exception("Brand not found");
            }

            // Cập nhật các thuộc tính của Influencer và User từ DTO
            brand.BrandName = requestDto.BrandName;
            brand.ImageCover = requestDto.ImageCover;
            brand.TaxCode = requestDto.TaxCode;
            brand.User.Email = requestDto.Email;
            brand.User.Bio = requestDto.Bio;
            brand.User.Phonenumber = requestDto.PhoneNumber;
            brand.User.Address = requestDto.Address;

            // Xử lý avatar
            if (requestDto.AvatarFile != null && requestDto.AvatarFile.Length > 0)
            {
                var avatarPath = await UploadAvatarAsync(brand.User.Avatar,requestDto.AvatarFile);
                brand.User.Avatar = avatarPath;
            }

            // Cập nhật thời gian sửa đổi
            brand.User.UpdatedAt = DateTime.Now;

            // Lưu thay đổi vào database
            await _context.SaveChangesAsync();
        }


        // Phương thức riêng xử lý việc upload avatar
        private async Task<string> UploadAvatarAsync(string currentAvatarPath,IFormFile avatarFile)
        {
            // Nếu không có file avatar mới, giữ nguyên avatar cũ
            if (avatarFile == null || avatarFile.Length == 0)
            {
                return currentAvatarPath;
            }

            // Nếu có avatar cũ, xóa nó
            if (!string.IsNullOrEmpty(currentAvatarPath))
            {
                await DeleteOldAvatarAsync(currentAvatarPath);
            }

            // Upload ảnh mới và lấy đường dẫn
            var avatarPath = await _fileService.UploadFileAsync(avatarFile,"avatars");
            return avatarPath;
        }

        private async Task DeleteOldAvatarAsync(string oldAvatarPath)
        {
            if (oldAvatarPath.Contains("avatars"))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(),"Uploads",oldAvatarPath.Replace("api\\",""));

                // Kiểm tra xem file cũ có tồn tại không, nếu có thì xóa
                if (File.Exists(fullPath))
                {
                    await _fileService.DeleteFileAsync(oldAvatarPath);
                }
                else
                {
                    Console.WriteLine($"File không tồn tại: {fullPath}");
                }
            }
        }

        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await GetByIdAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

    }
}