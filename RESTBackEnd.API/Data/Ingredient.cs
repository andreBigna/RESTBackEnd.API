using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace RESTBackEnd.API.Data
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Column(TypeName = "decimal(6,2)"), Required, Range(0, 999.99)]
        public double Amount { get; set; }

        [Required, ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }

        public Recipe? Recipe { get; set; }

        [Required, ForeignKey(nameof(UnitMeasure))]
        public int UnitMeasureId { get; set; }

        public UnitMeasure? UnitMeasure { get; set; }
    }
}
