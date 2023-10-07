using API.Helpers;
using API.Models.Entities;
using API.Repositories.Interfaces;
using Hospital.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {

        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddRoom(Room room)
        {
            _context.Add(room);
        }

        public void DeleteRoom(Room room)
        {
            _context.Remove(room);
        }

        public async Task<PagedList<Room>> GetAllRoomsWithUpComingAppointmentsAsync(RoomParams roomParams)
        {
            var query = _context.Rooms
                .Include(r => r.Doctor)
                    .ThenInclude(a => a.BookedWithAppointments
                        .Where(a => a.DateOfVisit > DateTime.Now))
                .AsQueryable();

            return await PagedList<Room>.CreateAsync(query, roomParams.PageNumber, roomParams.PageSize);
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            return await _context.Rooms
                .Where(r => r.Id == roomId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateRoom(Room room)
        {
            _context.Update(room);
        }
    }
}