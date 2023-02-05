using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Music.Store.Data.Entity;
using Music.Store.Infrastructure;
using Music.Store.Models;
using TestWebApi.Data;

namespace Music.Store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] CustomerFilterModel model)
        {
            var data = _context.Customers
                .Include(x => x.SupportRep)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Address))
            {
                data = data.Where(x => x.Address.ToLower().Contains(model.Address.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                data = data.Where(x => x.Email.ToLower().Contains(model.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                data = data.Where(x => x.FirstName.ToLower().Contains(model.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                data = data.Where(x => x.LastName.ToLower().Contains(model.LastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                data = data.Where(x => x.Phone.ToLower().Contains(model.Phone.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Company))
            {
                data = data.Where(x => x.Company.ToLower().Contains(model.Company.ToLower()));
            }
            var list = PaginationHelper<Customer>.Paginate(data, model);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer model)
        {
            await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer model)
        {
            _context.Customers.Update(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
