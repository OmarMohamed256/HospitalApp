namespace API.Models.DTOS
{
    public class CreateServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public int ServiceSpecialityId { get; set; }
        public List<CreateServiceInventoryItemDTO>? ServiceInventoryItems { get; set; }
    }
}