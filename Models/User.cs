using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMIApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role {get;set;} = "User";
        public DateTime CreatedAt { get; set; }

        // One User ke many Tasks
        public List<TaskItem> Tasks { get; set; }
    }
}