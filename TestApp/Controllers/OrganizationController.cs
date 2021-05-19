using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _db;

        public OrganizationController(OrganizationService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetAll()
        {
            var items = await _db.GetOrganizations();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var obj = await _db.GetOrganization(id);

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization obj)
        {
            await _db.Create(obj);

            return Ok(obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Organization item)
        {
            var obj = await _db.GetOrganization(id);

            if (obj == null)
                return NotFound();

            if (obj.Id == item.Id)
            {
                await _db.Update(item);

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _db.Remove(id);

            return Ok();
        }
    }
}
