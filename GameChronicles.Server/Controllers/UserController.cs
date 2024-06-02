using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Service;

namespace GameChronicles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var createdUser = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updated = await _userService.UpdateAsync(user);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
