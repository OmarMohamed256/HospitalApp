using webapi.Entities;

namespace API.Models.DTOS
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public int? DoctorSpecialityId { get; set; }
        public decimal? PriceVisit { get; set; }
        public decimal? PriceRevisit { get; set; }
        public DateTime DateCreated { get; set; }
        public bool LockoutEnabled { get; set; }
        public ICollection<UserRoleDto>? UserRoles { get; set; }
    }
}