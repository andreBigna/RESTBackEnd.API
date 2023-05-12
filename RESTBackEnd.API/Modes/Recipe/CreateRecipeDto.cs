using RESTBackEnd.API.Data;
using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Modes.Recipe
{
    public record CreateRecipeDto
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }
    }
}
