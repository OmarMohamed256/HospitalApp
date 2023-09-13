using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOS
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string FullName { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}