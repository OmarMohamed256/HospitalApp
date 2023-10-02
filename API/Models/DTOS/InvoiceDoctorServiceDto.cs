using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOS
{
    public class InvoiceDoctorServiceDto
    {
        
        public int Id { get; set; }
        public int? DoctorServiceId { get; set; }
        public int ServiceQuantity { get; set; }
    }
}