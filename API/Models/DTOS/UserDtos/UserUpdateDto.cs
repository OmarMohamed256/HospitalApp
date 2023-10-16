namespace API.Models.DTOS
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? PriceVisit { get; set; }
        public decimal? PriceRevisit { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}