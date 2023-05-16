using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Ingredient
{
	public record UpdateIngredientDto : BaseIngredientDto
	{
		[Required] public int IngredientId { get; set; }
	}
}