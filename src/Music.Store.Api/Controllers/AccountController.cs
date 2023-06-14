using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models.User;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(model);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _userService.Register(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var result = await _userService.ForgotPassword(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("ChangePasswordModel")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var result = await _userService.ChangePassword(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("EmailVerified/{code}")]
        public async Task<IActionResult> EmailVerified(string code)
        {
            var result = await _userService.EmailVerified(code);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var result = await _userService.ResetPassword(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetUserByCode/{code}")]
        public async Task<IActionResult> GetUserByCode(string code)
        {
            var result = await _userService.GetUserByCode(code);
            return Ok(result);
        }
    }
}
