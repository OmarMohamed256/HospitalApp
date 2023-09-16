
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        Task<IEnumerable<Speciality>> GetSpecialitiesAsync();
    }
}