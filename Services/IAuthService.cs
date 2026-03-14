using TMIApi.DTOs;

namespace TMIApi.Services
{
    public interface IAuthService
    {
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}