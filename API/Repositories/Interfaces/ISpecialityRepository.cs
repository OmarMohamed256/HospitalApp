
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> GetSpecialitiesAsync();
        void AddSpeciality(Speciality speciality);
        Task<bool> SaveAllAsync();
    }
}