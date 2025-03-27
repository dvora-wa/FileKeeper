using FileKeeper_server_.net.Core.Interfaces.Services;
using FileKeeper_server_.net.Core.Entities;
//using Core.Entities;
//using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            // שינויים כאן: שלח את ה-email וה-password בנפרד
            bool success = await _userService.RegisterUserAsync(user.Email, user.PasswordHash);
            if (!success) return BadRequest("Failed to register user");
            return Ok(new { message = "User registered successfully" });
        }
    }
}