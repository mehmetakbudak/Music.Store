using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Music.Store.Models;
using TestWebApi.Data;

namespace Music.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (employee == null)
            {
                return NotFound("Email veya şifre hatalıdır.");
            }




            return Ok();
        }
    }
}
