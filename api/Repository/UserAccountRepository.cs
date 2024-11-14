using api.Data;
using api.DTOs.LoginDTO;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public UserAccountRepository(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

            if (user == null)
            {
                return null;
            }

            return new LoginResponseDTO
            {
                Token = "GeneratedJWTToken",
                Role = user.Role,
                AccountId = user.UserId.ToString()
            };
        }
    }
}
