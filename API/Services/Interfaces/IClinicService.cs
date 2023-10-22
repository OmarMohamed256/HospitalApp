using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IClinicService
    {
        Task<ClinicDto> CreateUpdateClinic(CreateClinicDto clinic);
        Task<ClinicDto> GetClinicByIdAsync(int clinicId);
        Task<PagedList<ClinicDto>> GetAllClinicsAsync(ClinicParams clinicParams);
        Task DeleteClinic(int clinicId);
    }
}