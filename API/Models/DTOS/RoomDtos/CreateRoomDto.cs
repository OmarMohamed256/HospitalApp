namespace API.Models.DTOS
{
    public class CreateRoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomSpecialityId { get; set; }
        public int? DoctorId { get; set; }
    }
}