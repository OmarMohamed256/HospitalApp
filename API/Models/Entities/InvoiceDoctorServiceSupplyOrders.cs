using API.Models.Domains;

namespace API.Models.Entities
{
    public class InvoiceDoctorServiceSupplyOrders : SupplyOrderConsumerEntity
    {
        public int? InvoiceDoctorServiceId { get; set; }
        public virtual InvoiceDoctorService? InvoiceDoctorService {get; set;}
    }
}