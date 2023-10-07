using webapi.Entities;

namespace API.Models.DTOS
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomSpecialityId { get; set; }
        public int? DoctorId { get; set; }
        public RoomDoctorDto? Doctor { get; set; }
    }
}