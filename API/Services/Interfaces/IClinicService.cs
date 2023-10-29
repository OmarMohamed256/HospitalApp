using API.Helpers;
using API.Models.DTOS;

namespace API.Services.Interfaces
{
    public interface IClinicService
    {
        Task<ClinicDto> CreateUpdateClinic(CreateUpdateClinicDto clinic);
        Task<ClinicDto> GetClinicByIdAsync(int clinicId);
        Task<PagedList<ClinicDto>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync(ClinicParams clinicParams);
        Task<PagedList<ClinicDto>> GetClinics(ClinicParams clinicParams);
        Task DeleteClinic(int clinicId);
    }
}