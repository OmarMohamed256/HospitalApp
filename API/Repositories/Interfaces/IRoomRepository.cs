using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
        Task <Room> GetRoomById(int roomId);
        Task <ICollection<Room>> GetAllRoomsAsync();
        Task<bool> SaveAllAsync();
    }
}