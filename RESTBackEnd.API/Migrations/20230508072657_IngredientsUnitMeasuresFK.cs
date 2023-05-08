using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTBackEnd.API.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsUnitMeasuresFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitMeasureId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UnitMeasureId",
                table: "Ingredients",
                column: "UnitMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_UnitMeasures_UnitMeasureId",
                table: "Ingredients",
                column: "UnitMeasureId",
                principalTable: "UnitMeasures",
                principalColumn: "UnitMeasureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_UnitMeasures_UnitMeasureId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_UnitMeasureId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UnitMeasureId",
                table: "Ingredients");
        }
    }
}
