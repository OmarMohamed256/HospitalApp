using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> CreateUpdateRoom(CreateRoomDto room);
        Task<RoomDto> GetRoomByIdAsync(int roomId);
        Task<PagedList<RoomDto>> GetAllRoomsAsync(RoomParams roomParams);
        Task DeleteRoom(int roomId);
    }
}