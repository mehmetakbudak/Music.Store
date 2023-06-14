using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] CustomerFilterModel model)
        {
            var list = _customerService.GetByFilter(model);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer model)
        {
            var result = await _customerService.Post(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer model)
        {
            var result = await _customerService.Put(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
