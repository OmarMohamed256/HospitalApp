using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOS
{
    public class DoctorServiceUpdateDto
    {
        public int Id { get; set; }
        public decimal DoctorPercentage { get; set; }
        public decimal HospitalPercentage { get; set; }
    }
}