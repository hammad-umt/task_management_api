using System.ComponentModel.DataAnnotations;

namespace TMIApi.DTOs
{
    public class CreateTaskDto
    {
    // Title — Required, max 100 chars
        [Required]
        [StringLength(100)]
        public string? Title{get;set;}
    // Description — Optional, max 500 chars
        [StringLength(500)]
        public string? Description{get;set;}
    // Status — Optional
        public string? Status{get;set;}
    }

public class UpdateTaskDto
    {
    // Title — Required
    // Description — Optional
    // Status — Required
        [Required]
        [StringLength(100)]
        public string? Title{get;set;}
        [StringLength(500)]
        public string? Description{get;set;}
    
        [Required]
        public string? Status{get;set;}
    }

public class TaskResponseDto
    {
    // Id, Title, Description
    public int Id {get;set;}
    public string? Title{get;set;}
    public string? Description{get;set;}
    // Status, CreatedAt, UserId
    public string? Status {get;set;}
    public DateTime CreatedAt{get;set;}
    public int UserId{get;set;}
    }
}