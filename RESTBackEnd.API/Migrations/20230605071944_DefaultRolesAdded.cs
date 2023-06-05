using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RESTBackEnd.API.Migrations
{
    /// <inheritdoc />
    public partial class DefaultRolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2aa1f527-49b5-43e4-9695-92383555dd28", null, "Administrator", "ADMINISTRATOR" },
                    { "b6042cdc-550a-4dfe-a976-e0773c857076", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2aa1f527-49b5-43e4-9695-92383555dd28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6042cdc-550a-4dfe-a976-e0773c857076");
        }
    }
}
