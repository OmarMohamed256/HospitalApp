using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOS
{
    public class ServiceInventoryItemDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int InventoryItemId { get; set; }
        public int QuantityNeeded { get; set; }
        public InventoryItemDto? InventoryItem { get; set; }
    }
}