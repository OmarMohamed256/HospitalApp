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
        // Task <PagedList<Clinic>> GetAllClinicsWithUpComingAppointmentsAsync(ClinicParams clinicParams);
        Task <ICollection<Clinic>> GetClinicsWithFirstTwoUpcomingAppointmentsAsync();
        Task<bool> SaveAllAsync();
    }
}