using api.DTOs.LoginDTO;

namespace api.Repository
{
    public interface IUserAccountRepository
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest);
    }
}
