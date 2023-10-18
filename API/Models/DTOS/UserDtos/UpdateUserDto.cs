namespace API.Models.DTOS.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }
        public int? DoctorSpecialityId { get; set; }
        public ICollection<DoctorWorkingHoursDto>? DoctorWorkingHours { get; set; }
        public string? Role { get; set; }
        public decimal? PriceVisit { get; set; }
        public decimal? PriceRevisit { get; set; }
    }
}