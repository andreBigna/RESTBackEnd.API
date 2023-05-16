namespace RESTBackEnd.API.Models.Ingredient
{
	public record CreateIngredientDto : BaseIngredientDto
	{
		public int IngredientId { get; set; }
	}
}