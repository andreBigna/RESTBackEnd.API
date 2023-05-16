namespace RESTBackEnd.API.Models.Ingredient
{
	public record GetIngredientDto : BaseIngredientDto
	{
		public int IngredientId { get; set; }
	}
}