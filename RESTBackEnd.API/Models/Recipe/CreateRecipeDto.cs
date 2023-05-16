using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Recipe
{
	public record CreateRecipeDto : BaseRecipeDto
	{
		public IList<CreateIngredientDto>? Ingredients { get; set; }
	}
}