namespace RESTBackEnd.API.Models.Ingredient
{
    public class GetIngredientDto
    {
        public string? Name { get; set; }

        public double Amount { get; set; }

        public int UnitMeasureId { get; set; }
    }
}
