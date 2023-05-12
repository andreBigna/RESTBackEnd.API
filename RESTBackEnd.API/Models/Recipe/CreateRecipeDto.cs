using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;
using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Recipe
{
    public record CreateRecipeDto
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public IList<CreateIngredientDto>? Ingredients { get; set; }

    }
}
