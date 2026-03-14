using System.ComponentModel.DataAnnotations;

namespace TMIApi.DTOs
{
    public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; } = "User";
}

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
}