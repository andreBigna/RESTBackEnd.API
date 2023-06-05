using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace RESTBackEnd.API.Data.Configurations
{
	public class UnitMeasureConfiguration : IEntityTypeConfiguration<UnitMeasure>
	{
		public void Configure(EntityTypeBuilder<UnitMeasure> builder)
		{
			builder.HasIndex(u => u.Code)
				.IsUnique();

			builder.HasData(
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