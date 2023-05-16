namespace RESTBackEnd.API.Models.Recipe
{
	public record GetRecipeDto : BaseRecipeDto
	{
		public int RecipeId { get; set; }
	}
}