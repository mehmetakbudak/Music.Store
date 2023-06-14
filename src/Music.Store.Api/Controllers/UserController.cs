using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models.User;
using Music.Store.Service.Attributes;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
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

        [HttpGet("Profile")]
        [Authorize(CheckAccessRight = false)]
        public async Task<IActionResult> Profile()
        {
            var result = await _userService.GetProfile();
            return Ok(result);
        }

        [HttpPut("Profile")]
        [Authorize(CheckAccessRight = false)]
        public async Task<IActionResult> Profile([FromBody] ProfileModel model)
        {
            var result = await _userService.UpdateProfile(model);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UserModel model)
        {
            var result = await _userService.Post(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UserModel model)
        {
            var result = await _userService.Put(model);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
