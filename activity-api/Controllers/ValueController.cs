using System.Threading.Tasks;
using activity_data;
using activity_model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace activity_api.Controllers
{
    [ApiController]
    [Route("api/v1/values")]
    public class ValueController : ControllerBase
    {
        private readonly ActivityDataContext _context;

        public ValueController(ActivityDataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Values.ToListAsync());
        }

        [HttpGet("{id}",Name="getValueById")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x =>x.Id == id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Value model)
        {
            var value = new Value();
            value.Name = model.Name;
            await _context.AddAsync(value);
            await  _context.SaveChangesAsync();
            return Created("getValueById", null);
        }
    }
}