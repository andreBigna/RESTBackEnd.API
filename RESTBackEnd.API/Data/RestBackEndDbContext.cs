using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data.Configurations;

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

			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new UnitMeasureConfiguration());
		}
	}
}