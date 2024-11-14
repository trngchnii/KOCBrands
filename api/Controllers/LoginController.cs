using api.DTOs.LoginDTO;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly ITokenService _tokenService;

    public LoginController(IUserAccountRepository userAccountRepository, ITokenService tokenService)
    {
        _userAccountRepository = userAccountRepository;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        var user = await _userAccountRepository.LoginAsync(loginRequest);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var token = _tokenService.GenerateToken(user.AccountId, user.Role);

        return Ok(new LoginResponseDTO
        {
            Token = token,
            Role = user.Role,
            AccountId = user.AccountId.ToString()
        });
    }
}
