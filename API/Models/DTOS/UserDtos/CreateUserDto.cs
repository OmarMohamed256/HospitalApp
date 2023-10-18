
using System.ComponentModel.DataAnnotations;


namespace API.Models.DTOS
{
    public class CreateUserDto
    {
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public required string Gender { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
        public int? DoctorSpecialityId { get; set; }
        public ICollection<DoctorWorkingHoursDto>? DoctorWorkingHours { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public decimal? PriceVisit { get; set; }
        public decimal? PriceRevisit { get; set; }

    }
}