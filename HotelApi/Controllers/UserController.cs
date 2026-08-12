using HotelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllesrs
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userService)
        {

            _userservice = userService;

        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userservice.GetAllUser();
            return Ok(users);
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Models.DTOS.User.AddUserDto user)
        {
            await _userservice.AddUser(user);
            return Ok();
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userservice.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userservice.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] Models.DTOS.User.UpdateUserDto user)
        {
            var result = await _userservice.UpdateUser(id, user);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
