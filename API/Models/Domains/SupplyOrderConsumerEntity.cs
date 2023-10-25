using API.Models.Entities;

namespace API.Models.Domains
{
    public abstract class SupplyOrderConsumerEntity
    {
        public int Id { get; set; }
        public int? SupplyOrderId { get; set; }
        public virtual SupplyOrder SupplyOrder { get; set; }
        public int QuantityUsed { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}