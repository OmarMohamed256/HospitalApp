namespace API.Models.DTOS
{
    public class CreateClinicDto
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public int ClinicSpecialityId { get; set; }
        public int? DoctorId { get; set; }
    }
}