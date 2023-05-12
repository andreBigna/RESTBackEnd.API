using RESTBackEnd.API.Models.Ingredient;

namespace RESTBackEnd.API.Models.UnitMeasure
{
    public class GetUnitMeasureDto
    {
        public string? Code { get; set; }

        public string? LongName { get; set; }

        public IList<GetIngredientDto>? Ingredients { get; set; }
    }
}