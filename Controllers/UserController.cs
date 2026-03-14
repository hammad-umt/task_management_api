using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMIApi.DTOs;
using TMIApi.Services;
namespace TMIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
        
    public class UserController : ControllerBase{
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }    

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            await _userService.CreateAsync(dto);
            return Ok("User Created Successfully");
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDTO dto)
        {
            await _userService.UpdateAsync(id, dto);
            return Ok("User updated Successfully");
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
             return Ok("User Deleted Successfully");   
        }

        
    }
}