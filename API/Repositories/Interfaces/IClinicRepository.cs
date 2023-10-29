using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IClinicRepository
    {
        void AddClinic(Clinic clinic);
        void UpdateClinic(Clinic clinic);
        void DeleteClinic(Clinic clinic);
        Task <Clinic> GetClinicById(int clinicId);
        Task <PagedList<Clinic>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync(ClinicParams clinicParams);
        Task <PagedList<Clinic>> GetClinics(ClinicParams clinicParams);
        Task<bool> SaveAllAsync();
    }
}