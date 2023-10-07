using API.Models.DTOS;
using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IRoomService
    {
        Task<CreateRoomDto> CreateUpdateRoom(CreateRoomDto room);
        Task<CreateRoomDto> GetRoomByIdAsync(int roomId);
        Task<ICollection<CreateRoomDto>> GetAllRoomsAsync();
        Task<bool> DeleteRoom(int roomId);
    }
}