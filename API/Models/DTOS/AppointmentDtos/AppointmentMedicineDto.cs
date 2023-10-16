namespace API.Models.DTOS.AppointmentDtos
{
    public class AppointmentMedicineDto
    {
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public MedicineDto Medicine { get; set; }
    }
}