using RESTBackEnd.API.Models.Ingredient;

namespace RESTBackEnd.API.Models.Recipe
{
    public class GetRecipeDetailDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public IList<CreateIngredientDto>? Ingredients { get; set; }
    }
}