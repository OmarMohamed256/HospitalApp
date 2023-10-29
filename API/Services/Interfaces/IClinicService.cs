using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IClinicService
    {
        Task<ClinicDto> CreateUpdateClinic(CreateUpdateClinicDto clinic);
        Task<ClinicDto> GetClinicByIdAsync(int clinicId);
        Task<ICollection<ClinicDto>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync();
        Task DeleteClinic(int clinicId);
    }
}