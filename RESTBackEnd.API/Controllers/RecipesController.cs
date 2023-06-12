using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.Recipe;
using System.Reflection;

namespace RESTBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IRecipeRepository _recipeRepository;
		private readonly ILogger<RecipesController> _logger;

		public RecipesController(IMapper mapper, IRecipeRepository recipeRepository, ILogger<RecipesController> logger)
		{
			this._mapper = mapper;
			_recipeRepository = recipeRepository;
			_logger = logger;
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

			if (recipe == null) return ReturnRecipeNotFound(nameof(GetRecipe));

			var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

			return Ok(dtoRecipe);
		}

		// PUT: api/Recipes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id:int}")]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> PutRecipe(int id, UpdateRecipeDto updateRecipeDto)
		{
			const string methodName = nameof(PutRecipe);
			try
			{
				if (id != updateRecipeDto.RecipeId) return ReturnInvalidId();

				var recipe = await _recipeRepository.GetDetails(id);

				if (recipe == null) return ReturnRecipeNotFound(methodName);

				_mapper.Map(updateRecipeDto, recipe);

				await _recipeRepository.UpdateAsync(recipe);
			}
			catch (DbUpdateConcurrencyException e)
			{
				if (!await RecipeExists(id)) return ReturnRecipeNotFound(methodName);

				return ReturnInternalError(e, methodName);
			}
			catch (Exception e)
			{
				return ReturnInternalError(e, methodName);
			}

			return NoContent();
		}

		// POST: api/Recipes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<ActionResult<GetRecipeDetailDto>> PostRecipe(CreateRecipeDto createRecipeDto)
		{
			try
			{
				var recipe = _mapper.Map<Recipe>(createRecipeDto);

				await _recipeRepository.AddAsync(recipe);

				var dtoRecipe = _mapper.Map<GetRecipeDetailDto>(recipe);

				return Ok(dtoRecipe);
			}
			catch (Exception e)
			{
				return ReturnInternalError(e, nameof(PostRecipe));
			}
		}

		// DELETE: api/Recipes/5
		[HttpDelete("{id:int}")]
		[Authorize] //NO NEED TO USE ROLES FOR NOW, ANYWAY IT WOULD BE SOMETHING LIKE [Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteRecipe(int id)
		{
			if (!await RecipeExists(id)) return ReturnRecipeNotFound(nameof(DeleteRecipe));

			await _recipeRepository.DeleteAsync(id);

			return NoContent();
		}

		private async Task<bool> RecipeExists(int id)
		{
			return await _recipeRepository.ExistsAsync(id);
		}

		internal ActionResult ReturnRecipeNotFound(string methodName)
		{
			_logger.LogWarning($"{nameof(RecipesController)}.{methodName} recipe not found.");
			return NotFound();
		}

		internal IActionResult ReturnInvalidId()
		{
			_logger.LogWarning($"{nameof(RecipesController)}.{nameof(PutRecipe)} recipe with invalid ID.");
			return BadRequest();
		}

		internal ActionResult ReturnInternalError(Exception e, string methodName)
		{
			_logger.LogError(e, $"Error calling {nameof(RecipesController)}.{methodName}");

			return Problem("Something went wrong, please contact support", statusCode: 500);
		}
	}
}