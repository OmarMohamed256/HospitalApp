
using API.Models.Domains;

namespace API.Models.Entities
{
    // consumes from supply orders
    public class SellOrder : OrderEntity
    {
        public string? SoldTo { get; set; }
        public bool IncludeExpiredItems { get; set; }
    }
}