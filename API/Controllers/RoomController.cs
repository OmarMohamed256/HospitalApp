using API.Models.DTOS;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RoomController : BaseApiController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CreateRoomDto>>> GetRoomsAsync()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<ActionResult<CreateRoomDto>> CreateRoomAsync(CreateRoomDto room)
        {
            var newRoom = await _roomService.CreateUpdateRoom(room);
            return Ok(newRoom);
        }
        [HttpPut]
        public async Task<ActionResult<CreateRoomDto>> UpdateRoomAsync(CreateRoomDto room)
        {
            var newRoom = await _roomService.CreateUpdateRoom(room);
            return Ok(newRoom);
        }
    }
}