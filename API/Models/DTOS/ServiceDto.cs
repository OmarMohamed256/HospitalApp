using HospitalApp.Models.Entities;

namespace API.Models.DTOS
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DisposablesPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ServiceSpecialityId { get; set; }
    }
}