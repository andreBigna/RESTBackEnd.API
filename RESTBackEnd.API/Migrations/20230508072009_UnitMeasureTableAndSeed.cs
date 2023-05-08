using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RESTBackEnd.API.Migrations
{
    /// <inheritdoc />
    public partial class UnitMeasureTableAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    UnitMeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LongName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.UnitMeasureId);
                });

            migrationBuilder.InsertData(
                table: "UnitMeasures",
                columns: new[] { "UnitMeasureId", "Code", "LongName" },
                values: new object[,]
                {
                    { 1, "mL", "milliliters" },
                    { 2, "L", "liters" },
                    { 3, "mg", "milligrams" },
                    { 4, "g", "grams" },
                    { 5, "kg", "kilograms" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitMeasures_Code",
                table: "UnitMeasures",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitMeasures");
        }
    }
}
