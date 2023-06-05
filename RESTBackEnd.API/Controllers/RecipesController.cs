using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.Recipe;

namespace RESTBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IRecipeRepository _recipeRepository;

		public RecipesController(IMapper mapper, IRecipeRepository recipeRepository)
		{
			this._mapper = mapper;
			_recipeRepository = recipeRepository;
		}

		// GET: api/Recipes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetRecipeDto>>> GetRecipes()
		{
			var recipes = await _recipeRepository.GetAllAsync();
			var dtoRecipes = _mapper.Map<IEnumerable<GetRecipeDto>>(recipes);
			return Ok(dtoRecipes);
		}

		// GET: api/Recipes/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<GetRecipeDetailDto>> GetRecipe(int id)
		{
			var recipe = await _recipeRepository.GetDetails(id);

			if (recipe == null) return NotFound();

			var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

			return Ok(dtoRecipe);
		}

		// PUT: api/Recipes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:int}")]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> PutRecipe(int id, UpdateRecipeDto updateRecipeDto)
		{
			if (id != updateRecipeDto.RecipeId) return BadRequest();

			var recipe = await _recipeRepository.GetDetails(id);

			if (recipe == null) return NotFound();

			_mapper.Map(updateRecipeDto, recipe);

			try
			{
				await _recipeRepository.UpdateAsync(recipe);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await RecipeExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// POST: api/Recipes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<ActionResult<GetRecipeDetailDto>> PostRecipe(CreateRecipeDto createRecipeDto)
		{
			var recipe = _mapper.Map<Recipe>(createRecipeDto);

			await _recipeRepository.AddAsync(recipe);

			var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

			return Ok(dtoRecipe);
		}

		// DELETE: api/Recipes/5
		[HttpDelete("{id:int}")]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteRecipe(int id)
		{
			if (!await RecipeExists(id)) return NotFound();

			await _recipeRepository.DeleteAsync(id);

			return NoContent();
		}

		private async Task<bool> RecipeExists(int id)
		{
			return await _recipeRepository.ExistsAsync(id);
		}
	}
}