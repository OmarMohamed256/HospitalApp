using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOS.UserDtos
{
    public class ChangeRoleDto
    {
        public string CurrentRole { get; set; }
        public string NewRole { get; set; }
    }
}