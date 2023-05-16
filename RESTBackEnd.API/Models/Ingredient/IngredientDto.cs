namespace RESTBackEnd.API.Models.Ingredient
{
	public record IngredientDto
	{
		public int IngredientId { get; set; }

		public string? Name { get; set; }

		public double Amount { get; set; }

		public int RecipeId { get; set; }

		public int UnitMeasureId { get; set; }
	}
}