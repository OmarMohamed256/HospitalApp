namespace API.Models.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string ClinicNumber { get; set; }
        public virtual ICollection<ClinicDoctor>? ClinicDoctors { get; set; }
    }
}