using API.Models.Domains;

namespace API.Models.Entities
{
    public class SellOrderConsumesSupplyOrder : SupplyOrderConsumerEntity
    {
        public int? SellOrderId { get; set; }
        public virtual SellOrder SellOrder { get; set; }
    }
}