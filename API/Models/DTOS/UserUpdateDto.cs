namespace API.Models.DTOS
{
    public class UserUpdateDto
    {
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}