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

        public async Task<(Brand?, User?)> UpdateAsync(int brandId,UpdateBrandUserRequestDto brandModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Tìm Brand hiện có theo ID
                var existingBrand = await _context.Brands
                    .Include(b => b.User)  // Đảm bảo User được bao gồm
                    .FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (existingBrand == null)
                {
                    return (null, null);
                }

                string oldImageCover = existingBrand.ImageCover;
                string oldAvatar = existingBrand.User.Avatar;

                // Kiểm tra và lưu file ảnh đại diện của Brand
                if (brandModel.Brand.ImageFile != null)
                {
                    if (brandModel.Brand.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new Exception("Kích thước file hình ảnh không được vượt quá 1 MB.");
                    }

                    string[] allowedFileExtensions = { ".jpg",".jpeg",".png" };
                    string createdImageName = await _fileService.SaveFileAsync(brandModel.Brand.ImageFile,allowedFileExtensions);
                    existingBrand.ImageCover = createdImageName; // Cập nhật ảnh đại diện
                }

                // Cập nhật các thuộc tính của Brand
                existingBrand.BrandName = brandModel.Brand.BrandName;
                existingBrand.TaxCode = brandModel.Brand.TaxCode;

                // Cập nhật thông tin User
                if (existingBrand.User != null)
                {
                    existingBrand.User.Email = brandModel.User.Email;
                    existingBrand.User.Bio = brandModel.User.Bio;
                    existingBrand.User.Phonenumber = brandModel.User.Phonenumber;
                    existingBrand.User.Address = brandModel.User.Address;

                    // Kiểm tra và lưu file ảnh đại diện của User
                    if (brandModel.User.AvatarFile != null)
                    {
                        if (brandModel.User.AvatarFile.Length > 1 * 1024 * 1024)
                        {
                            throw new Exception("Kích thước file ảnh đại diện không được vượt quá 1 MB.");
                        }
                        string[] allowedFileExtensions = { ".jpg",".jpeg",".png" };
                        string avatarCreatedName = await _fileService.SaveFileAsync(brandModel.User.AvatarFile,allowedFileExtensions);
                        existingBrand.User.Avatar = avatarCreatedName; // Cập nhật ảnh đại diện
                    }
                }

                // Lưu các thay đổi cho cả Brand và User
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Xóa file cũ nếu đã có ảnh mới
                if (brandModel.Brand.ImageFile != null)
                {
                    _fileService.DeleteFile(oldImageCover);
                }
                if (brandModel.User.AvatarFile != null)
                {
                    _fileService.DeleteFile(oldAvatar);
                }

                return (existingBrand, existingBrand.User);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Có lỗi xảy ra trong quá trình cập nhật: " + ex.Message);
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