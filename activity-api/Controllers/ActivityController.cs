using System.Threading.Tasks;
using activity_data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace activity_api.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityDataContext _context;

        public ActivityController(ActivityDataContext context)
        {
            _context = context;
        }

         [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Values.ToListAsync());
        }
    }

   
}