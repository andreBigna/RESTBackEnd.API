using RESTBackEnd.API.Models.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.UnitMeasure
{
	public abstract record BaseUnitMeasureDto
	{
		[Required, MaxLength(5)] public string? Code { get; set; }

		[Required] public string? LongName { get; set; }

		public IList<GetIngredientDto>? Ingredients { get; set; }
	}
}