using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface ISpecialityService
    {
        Task<IEnumerable<SpecialityDto>> GetAllSpecialitiesAsync();
    }
}