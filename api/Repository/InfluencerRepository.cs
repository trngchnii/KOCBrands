using api.Data;
using api.DTOs;
using api.Models;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InfluencerRepository : IInfluencerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public InfluencerRepository(ApplicationDbContext context,IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task AddAsync(Influencer influencer)
        {
            _context.Influencers.Add(influencer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var influencer = await GetByIdAsync(id);
            if (influencer != null)
            {
                _context.Influencers.Remove(influencer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Influencer>> GetAllAsync()
        {
            return await _context.Influencers.Include(i => i.User).ToListAsync();
        }

        public async Task<Influencer?> GetByIdAsync(int id)
        {
            return await _context.Influencers.Include(i => i.User).FirstOrDefaultAsync(i => i.InfluencerId == id);
        }

        public async Task UpdateInfluencerAsync(int influencerId,UpdateInfluencerRequestDto requestDto)
        {
            try
            {
                // Tìm influencer theo InfluencerId
                var influencer = await _context.Influencers
                    .Include(i => i.User) // Include User để cập nhật Avatar
                    .FirstOrDefaultAsync(i => i.InfluencerId == influencerId);

                if (influencer == null)
                {
                    throw new Exception("Influencer not found");
                }

                // Cập nhật các thuộc tính của Influencer và User từ DTO
                influencer.Name = requestDto.Name;
                influencer.Gender = requestDto.Gender ?? "Nam";
                influencer.DateOfBirth = requestDto.DateOfBirth;
                influencer.BookingPrice = requestDto.BookingPrice;
                influencer.PersonalIdentificationNumber = requestDto.PersonalIdentificationNumber;
                influencer.User.Email = requestDto.Email;
                influencer.User.Bio = requestDto.Bio;
                influencer.User.Phonenumber = requestDto.PhoneNumber;
                influencer.User.Address = requestDto.Address;

                // Xử lý avatar
                if (requestDto.AvatarFile != null && requestDto.AvatarFile.Length > 0)
                {
                    // Upload ảnh mới và lấy đường dẫn
                    var avatarPath = await UploadAvatarAsync(influencer.User.Avatar,requestDto.AvatarFile);
                    influencer.User.Avatar = avatarPath; // Cập nhật đường dẫn avatar vào User
                }

                // Cập nhật thời gian sửa đổi
                influencer.User.UpdatedAt = DateTime.Now;

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
    }