using HospitalApp.Models.Entities;

namespace API.Models.Entities
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InventoryItemSpecialityId { get; set; }
        public virtual Speciality InventoryItemSpeciality { get; set; }
        public virtual ICollection<SupplyOrder>? SupplyOrders { get; set; }
        public virtual ICollection<SellOrder>? SellOrders { get; set; }

        public ICollection<ServiceInventoryItem>? ServiceInventoryItems { get; set; }

    }
}