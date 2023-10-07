using API.Helpers;
using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<CreateRoomDto> CreateUpdateRoom(CreateRoomDto room)
        {
            var newRoom = _mapper.Map<Room>(room);
            var oldRoom = await _roomRepository.GetRoomById(room.Id);
            if (oldRoom == null)
                _roomRepository.AddRoom(newRoom);
            else
                _roomRepository.UpdateRoom(newRoom);
            var result = await _roomRepository.SaveAllAsync();
            if (!result) throw new Exception("Failed to add/update room");
            return _mapper.Map<CreateRoomDto>(newRoom);
        }

        public async Task<bool> DeleteRoom(int roomId)
        {
            var room = await _roomRepository.GetRoomById(roomId) ?? throw new Exception("Room not found");
            _roomRepository.DeleteRoom(room);
            var deleteResult = await _roomRepository.SaveAllAsync();
            return deleteResult;
        }

        public async Task<PagedList<RoomDto>> GetAllRoomsAsync(RoomParams roomParams)
        {
            PagedList<Room> rooms = await _roomRepository.GetAllRoomsWithUpComingAppointmentsAsync(roomParams);

            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return new PagedList<RoomDto>(roomsDto, rooms.TotalCount, rooms.CurrentPage, rooms.PageSize);
        }

        public async Task<RoomDto> GetRoomByIdAsync(int roomId)
        {
            var room = await _roomRepository.GetRoomById(roomId) ?? throw new Exception("Room not found");
            return _mapper.Map<RoomDto>(room);
        }
    }
}