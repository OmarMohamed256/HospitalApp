using API.Helpers;
using API.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
        Task <Room> GetRoomById(int roomId);
        Task <Room> GetRoomByDoctorId(int doctorId);
        Task <PagedList<Room>> GetAllRoomsWithUpComingAppointmentsAsync(RoomParams roomParams);
        Task<bool> SaveAllAsync();
    }
}