#nullable disable
using Microsoft.AspNetCore.Mvc;
using rapid_news_media_auth_api.Models;
using rapid_news_media_auth_api.Authorization;
using rapid_news_media_auth_api.Services;

namespace rapid_news_media_auth_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IAuthService _authService;

        private IUserService _userService;

        public UsersController(IAuthService authService, IUserService userService)
        {
            _authService = authService;

            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            //return await _context.Users.ToListAsync();
            return await _userService.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedUser = await _userService.Update(user);
                if (updatedUser == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            var createdUser = await _userService.Create(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.Delete(user);

            return NoContent();
        }

    }
}
