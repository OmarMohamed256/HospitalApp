using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOS
{
    public class CreateCustomItemDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
    }
}