using API.Models.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface ISpecialityService
    {
        Task<IEnumerable<SpecialityDto>> GetAllSpecialitiesAsync();
        Task<SpecialityDto> AddSpeciality(SpecialityDto specialityDto);

    }
}