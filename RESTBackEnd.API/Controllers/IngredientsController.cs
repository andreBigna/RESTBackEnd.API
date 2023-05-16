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
using RESTBackEnd.API.Models.Recipe;

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
			var ingredients = await _context.Ingredients.ToListAsync();
			var dtoIngredients = _mapper.Map<IEnumerable<GetIngredientDto>>(ingredients);

			return Ok(dtoIngredients);
		}

		// GET: api/Ingredients/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<GetIngredientDto>> GetIngredient(int id)
		{
			var ingredient = await _context.Ingredients.FindAsync(id);

			if (ingredient == null) return NotFound();
			var dtoIngredient = _mapper.Map<GetIngredientDto>(ingredient);

			return Ok(dtoIngredient);
		}

		// PUT: api/Ingredients/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutIngredient(int id, UpdateIngredientDto updateIngredientDto)
		{
			if (id != updateIngredientDto.IngredientId) return BadRequest();

			var ingredient = await _context.Ingredients.FindAsync(id);

			if (ingredient == null) return NotFound();

			_mapper.Map(updateIngredientDto, ingredient);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!IngredientExists(id))
					return NotFound();
				else
					return Problem("Error updating the updateIngredientDto");
			}

			return NoContent();
		}

		// POST: api/Ingredients
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<CreateIngredientDto>> PostIngredient(CreateIngredientDto createIngredientDto)
		{
			var ingredient = _mapper.Map<Ingredient>(createIngredientDto);

			_context.Ingredients.Add(ingredient);

			await _context.SaveChangesAsync();

			return Ok(createIngredientDto);
		}

		// DELETE: api/Ingredients/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteIngredient(int id)
		{
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