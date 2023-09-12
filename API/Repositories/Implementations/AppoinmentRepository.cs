using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace API.Repositories.Implementations
{
    public class AppoinmentRepository : IAppoinmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppoinmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // public async Task<IEnumerable<AppUser>> GetUsersWithAppointmentsForDoctor(int doctorId)
        // {
        //     return await _context.Appointments
        //         .AsNoTracking()
        //         .Where(a => a.DoctorId == doctorId)
        //         .Select(a => a.User)
        //         .Distinct()
        //         .ToListAsync();
        // }
    }
}