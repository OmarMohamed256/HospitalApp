using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using HospitalApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly ApplicationDbContext _context;
        public SpecialityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSpeciality(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
        }

        public async Task<IEnumerable<Speciality>> GetSpecialitiesAsync()
        {
            return await _context.Specialities.ToListAsync();
        }

        public async Task<Speciality> GetSpecialityByIdAsync(int specialityId)
        {
            return await _context.Specialities
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == specialityId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateSpeciality(Speciality speciality)
        {
            _context.Specialities.Update(speciality);
        }
    }
}