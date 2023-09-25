using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class DoctorWorkingHoursRepository : IDoctorWorkingHoursRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DoctorWorkingHoursRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<DoctorWorkingHours>> GetDoctorWorkingHoursByDoctorIdAsync(int doctorId)
        {
            return await _applicationDbContext.DoctorWorkingHours.Where(dwh => dwh.DoctorId == doctorId).ToListAsync();
        }
    }
}