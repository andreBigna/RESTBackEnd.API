using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTBackEnd.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewUnitMeasure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UnitMeasures",
                columns: new[] { "UnitMeasureId", "Code", "LongName" },
                values: new object[] { 6, "pcs", "pieces" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UnitMeasures",
                keyColumn: "UnitMeasureId",
                keyValue: 6);
        }
    }
}
