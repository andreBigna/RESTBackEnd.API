using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Recipe;

namespace RESTBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController : ControllerBase
	{
		private readonly RestBackEndDbContext _context;
		private readonly IMapper _mapper;

		public RecipesController(RestBackEndDbContext context, IMapper mapper)
		{
			_context = context;
			this._mapper = mapper;
		}

		// GET: api/Recipes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetRecipes()
		{
			var recipes = await _context.Recipes.ToListAsync();
			var dtoRecipes = _mapper.Map<IEnumerable<GetRecipeDto>>(recipes);
			return Ok(dtoRecipes);
		}

		// GET: api/Recipes/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<GetRecipeDetailDto>> GetRecipe(int id)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null) return NotFound();

			var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

			return Ok(dtoRecipe);
		}

		// PUT: api/Recipes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutRecipe(int id, UpdateRecipeDto updateRecipeDto)
		{
			if (id != updateRecipeDto.RecipeId) return BadRequest();

			var recipe = await _context.Recipes.FindAsync(id);

			if (recipe == null) return NotFound();

			_mapper.Map(updateRecipeDto, recipe);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RecipeExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// POST: api/Recipes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<GetRecipeDetailDto>> PostRecipe(CreateRecipeDto createRecipeDto)
		{
			var recipe = _mapper.Map<Recipe>(createRecipeDto);

			_context.Recipes.Add(recipe);
			await _context.SaveChangesAsync();

			var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

			return Ok(dtoRecipe);
		}

		// DELETE: api/Recipes/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteRecipe(int id)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null) return NotFound();

			_context.Recipes.Remove(recipe);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool RecipeExists(int id)
		{
			return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
		}
	}
}