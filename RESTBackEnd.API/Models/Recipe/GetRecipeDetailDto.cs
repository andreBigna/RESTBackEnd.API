using RESTBackEnd.API.Models.Ingredient;

namespace RESTBackEnd.API.Models.Recipe
{
	public record GetRecipeDetailDto : BaseRecipeDto
	{
		public int RecipeId { get; set; }

		public IList<GetIngredientDto>? Ingredients { get; set; }
	}
}