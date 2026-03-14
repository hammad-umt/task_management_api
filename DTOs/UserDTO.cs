using System.ComponentModel.DataAnnotations;

namespace TMIApi.DTOs
{
    public class UserResponseDTO
    {
        public int Id {get;set;}
        public string? Name{get;set;}
        public string? Email{get;set;}
        public DateTime CreatedAt{get;set;}
    }
    public class CreateUserDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
       public string? Name ;
    [Required]
    [EmailAddress]
    public string? Email; 
    [Required]
    [StringLength(20, MinimumLength = 8)]
    public string? Password; 
    }
    public class UpdateUserDTO
    {
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    }
}