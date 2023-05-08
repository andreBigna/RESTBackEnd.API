using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RESTBackEnd.API.Data
{
	public class Recipe
	{

		[Key]
		public int RecipeId { get; set; }

		[Required, MaxLength(50)]
		public string? Name { get; set; }

		public string? Description { get; set; }

		public string? ImagePath { get; set; }

		public IList<Ingredient>? Ingredients { get; set; }
	}
}
