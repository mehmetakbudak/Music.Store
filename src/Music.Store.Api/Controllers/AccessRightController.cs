using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models.AccessRight;
using Music.Store.Service.Attributes;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRightController : ControllerBase
    {
        private readonly IAccessRightService _accessRightService;

        public AccessRightController(IAccessRightService accessRightService)
        {
            _accessRightService = accessRightService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var result = await _accessRightService.Get();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _accessRightService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AccessRightModel model)
        {
            var result = await _accessRightService.Post(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] AccessRightModel model)
        {
            var result = await _accessRightService.Put(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accessRightService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
