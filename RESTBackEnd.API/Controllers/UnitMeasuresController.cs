using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;

namespace RESTBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitMeasuresController : ControllerBase
    {
        private readonly RestBackEndDbContext _context;

        public UnitMeasuresController(RestBackEndDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitMeasures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitMeasure>>> GetUnitMeasures()
        {
            if (_context.UnitMeasures == null) return NotFound();

            return Ok(await _context.UnitMeasures.ToListAsync());
        }

        // GET: api/UnitMeasures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitMeasure>> GetUnitMeasure(int id)
        {
            if (_context.UnitMeasures == null) return NotFound();

            var unitMeasure = await _context.UnitMeasures.FindAsync(id);

            if (unitMeasure == null) return NotFound();

            return Ok(unitMeasure);
        }

        // PUT: api/UnitMeasures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitMeasure(int id, UnitMeasure unitMeasure)
        {
            if (id != unitMeasure.UnitMeasureId) return BadRequest();

            _context.Entry(unitMeasure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitMeasureExists(id)) 
                  return NotFound();
                else
                  throw;
            }

            return NoContent();
        }

        // POST: api/UnitMeasures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitMeasure>> PostUnitMeasure(UnitMeasure unitMeasure)
        {
            if (_context.UnitMeasures == null) return Problem("Entity set 'RestBackEndDbContext.UnitMeasures'  is null.");
            _context.UnitMeasures.Add(unitMeasure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitMeasure", new { id = unitMeasure.UnitMeasureId }, unitMeasure);
        }

        // DELETE: api/UnitMeasures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitMeasure(int id)
        {
            if (_context.UnitMeasures == null) return NotFound();
            
            var unitMeasure = await _context.UnitMeasures.FindAsync(id);
            if (unitMeasure == null) return NotFound();

            _context.UnitMeasures.Remove(unitMeasure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitMeasureExists(int id)
        {
            return (_context.UnitMeasures?.Any(e => e.UnitMeasureId == id)).GetValueOrDefault();
        }
    }
}
