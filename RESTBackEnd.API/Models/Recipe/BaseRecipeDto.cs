using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Recipe
{
	public abstract record BaseRecipeDto
	{
		[Required, MaxLength(50)] public string? Name { get; set; }

		public string? Description { get; set; }

		public string? ImagePath { get; set; }
	}
}