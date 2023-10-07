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

        public async Task<ICollection<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
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