using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models.UserAccessRight;
using Music.Store.Service.Attributes;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessRightController : ControllerBase
    {
        private readonly IUserAccessRightService _userAccessRightService;

        public UserAccessRightController(IUserAccessRightService userAccessRightService)
        {
            _userAccessRightService = userAccessRightService;
        }

        [Authorize]
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _userAccessRightService.GetByUserId(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("UserAccessRightCreateOrUpdate")]
        public async Task<IActionResult> Post([FromBody] UserAccessRightModel model)
        {
            var result = await _userAccessRightService.CreateOrUpdate(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
