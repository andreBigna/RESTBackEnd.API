using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;

namespace RESTBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly RestBackEndDbContext _context;
        private readonly IMapper _mapper;

        public IngredientsController(RestBackEndDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetIngredientDto>>> GetIngredients()
        {
            if (_context.Ingredients == null) return NotFound();

            var ingredients = await _context.Ingredients.ToListAsync();
            var dtoIngredients = _mapper.Map<IEnumerable<GetIngredientDto>>(ingredients);

            return Ok(dtoIngredients);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetIngredientDto>> GetIngredient(int id)
        {
            if (_context.Ingredients == null) return NotFound();

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null) return NotFound();
            var dtoIngredient = _mapper.Map<GetIngredientDto>(ingredient);

            return Ok(dtoIngredient);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.IngredientId) return BadRequest();

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                    return NotFound();
                else
                    return Problem("Error updating the ingredient");
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            if (_context.Ingredients == null) return Problem("Entity set 'RestBackEndDbContext.Ingredients'  is null.");

            _context.Ingredients.Add(ingredient);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null) return NotFound();

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null) return NotFound();

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
