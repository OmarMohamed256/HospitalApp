using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOS
{
    public class LoginDto
    {
        [Required] public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}