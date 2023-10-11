
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> GetSpecialitiesAsync();
        Task<Speciality> GetSpecialityByIdAsync(int specialityId);
        void AddSpeciality(Speciality speciality);
        void UpdateSpeciality(Speciality speciality);
        Task<bool> SaveAllAsync();
    }
}