using API.Extenstions;
using API.Helpers;
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
        public async Task<ActionResult<ICollection<CreateRoomDto>>> GetRoomsAsync([FromQuery] RoomParams roomParams)
        {
            var rooms = await _roomService.GetAllRoomsAsync(roomParams);
            Response.AddPaginationHeader(rooms.CurrentPage, rooms.PageSize, rooms.TotalCount, rooms.TotalPages);
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