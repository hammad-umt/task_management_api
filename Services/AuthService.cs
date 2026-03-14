using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TMIApi.DTOs;
using TMIApi.Models;
using TMIApi.Repositories;

namespace TMIApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config= configuration;
        }
    private static string NormalizeRole(string? role)
    {
        var value = (role ?? string.Empty).Trim();

        if (value.Equals("admin", StringComparison.OrdinalIgnoreCase))
            return "Admin";

        if (value.Equals("user", StringComparison.OrdinalIgnoreCase))
            return "User";

        return string.IsNullOrWhiteSpace(value) ? "User" : value;
    }

    public string generateToken(User user)
    {
            var normalizedRole = NormalizeRole(user.Role);
            var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, normalizedRole)
        };
        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims : claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
{
    // 1. Email check
    var existing = await _userRepository.GetByEmailAsync(dto.Email);
    if (existing != null)
        throw new Exception("Email already exist hai");

    // 2. User banao
    var user = new User
    {
        Name = dto.Name,
        Email = dto.Email,
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        Role = "User",
        CreatedAt = DateTime.Now
    };

    // 3. Save karo
    await _userRepository.AddAsync(user);

    // 4. Token generate karo
    var token = generateToken(user);

    // 5. Response return karo
    return new AuthResponseDto
    {
        Token = token,
        Name = user.Name,
        Email = user.Email
    };
}
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
{
    // 1. User dhundo
    var user = await _userRepository.GetByEmailAsync(dto.Email);
    if (user == null)
        throw new Exception("Incorrect Credentials");

    // 2. Password verify karo
    var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
    if (!isValid)
        throw new Exception("Incorrect Credentials");

    // 3. Token generate karo aur return karo
    return new AuthResponseDto
    {
        Token = generateToken(user),
        Name = user.Name,
        Email = user.Email
    };
}
        }
    }
