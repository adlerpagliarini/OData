using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData.Data;
using OData.Domain;

namespace OData.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DeveloperController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Developer developer)
        {
            _context.Developer.Add(developer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = developer.Id }, developer);
        }

        [HttpGet]
        [Route("{id}")]
        [EnableQuery()]
        public async Task<ActionResult<Developer>> Get(int id)
        {
            var developer = await _context.Developer.FindAsync(id);
            if (developer == null) return NotFound();
            return developer;
        }

        [HttpGet]
        [EnableQuery()]
        public IEnumerable<Developer> Get() => _context.Developer.AsQueryable();

    }
}
