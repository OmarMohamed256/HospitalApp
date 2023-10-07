using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IRoomService
    {
        Task<CreateRoomDto> CreateUpdateRoom(CreateRoomDto room);
        Task<CreateRoomDto> GetRoomByIdAsync(int roomId);
        Task<PagedList<CreateRoomDto>> GetAllRoomsAsync(RoomParams roomParams);
        Task<bool> DeleteRoom(int roomId);
    }
}