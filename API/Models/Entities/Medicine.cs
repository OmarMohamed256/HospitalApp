namespace API.Models.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public virtual ICollection<AppointmentMedicine>? AppointmentMedicines { get; set; }
    }
}