namespace API.Models.Entities
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AppointmentMedicine>? AppointmentMedicines { get; set; }
    }
}