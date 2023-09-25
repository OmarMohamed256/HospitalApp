using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Entities
{
    public class InvoiceDoctorServiceSupplyOrders
    {
        public int Id { get; set; }
        public int? InvoiceDoctorServiceId { get; set; }
        public virtual InvoiceDoctorService? InvoiceDoctorService {get; set;}
        public int? SupplyOrderId { get; set; }
        public virtual SupplyOrder? SupplyOrder {get; set;}
        public int  QuantityUsed { get; set; }
        public int  ItemPrice { get; set; }
        public int  TotalPrice { get; set; }
    }
}