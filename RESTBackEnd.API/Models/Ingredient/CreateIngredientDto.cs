using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Ingredient
{
    public class CreateIngredientDto
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required, Range(0, 999.99)]
        public double Amount { get; set; }

        [Required]
        public int UnitMeasureId { get; set; }
    }
}