using RESTBackEnd.API.Models.Ingredient;

namespace RESTBackEnd.API.Models.Recipe
{
	public record UpdateRecipeDto : BaseRecipeDto
	{
		public int RecipeId { get; set; }
		public IList<UpdateIngredientDto>? Ingredients { get; set; }
	}
}