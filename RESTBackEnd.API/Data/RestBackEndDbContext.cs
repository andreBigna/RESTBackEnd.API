using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RESTBackEnd.API.Data
{
	public class RestBackEndDbContext : IdentityDbContext
	{
		public RestBackEndDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<UnitMeasure> UnitMeasures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UnitMeasure>()
				.HasIndex(u => u.Code)
				.IsUnique();

			modelBuilder.Entity<UnitMeasure>().HasData(
				new UnitMeasure
				{
					UnitMeasureId = 1,
					Code = "mL",
					LongName = "milliliters"
				},
				new UnitMeasure
				{
					UnitMeasureId = 2,
					Code = "L",
					LongName = "liters"
				},
				new UnitMeasure
				{
					UnitMeasureId = 3,
					Code = "mg",
					LongName = "milligrams"
				},
				new UnitMeasure
				{
					UnitMeasureId = 4,
					Code = "g",
					LongName = "grams"
				},
				new UnitMeasure
				{
					UnitMeasureId = 5,
					Code = "kg",
					LongName = "kilograms"
				},
				new UnitMeasure
				{
					UnitMeasureId = 6,
					Code = "pcs",
					LongName = "pieces"
				}
			);
		}
	}
}