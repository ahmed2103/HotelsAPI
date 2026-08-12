using HotelServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Booking;
using Models.DTOS.Room;
using Models.Entities;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public readonly IRoomService _roomService;
        public RoomController(IRoomService roomService) {
        
             _roomService = roomService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllRooms();
            return Ok(rooms);
        }
        [HttpGet("GetRoomById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> Create([FromForm] CreateRoomDto room)
        {
            var createdRoom = await _roomService.CreateRoom(room);
            return Ok(createdRoom);

        }
        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateRoomDto updatedRoom)
        {
            var result = await _roomService.UpdateRoom(id, updatedRoom);
            if (!result)
            {
                return NotFound();
            }
            return Ok(updatedRoom);
        }
        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delteRoom = await _roomService.GetRoomById(id);
            var result = await _roomService.DeleteRoom(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(delteRoom);
        }
    }
}
