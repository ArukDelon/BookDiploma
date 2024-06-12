using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class CaticonInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11a9bf1c-157b-4f73-98cb-2ff71fd24783");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f6a931-f961-4986-95fe-c68f53f671af");

            migrationBuilder.AddColumn<string>(
                name: "FaIcon",
                table: "category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "187dea59-9896-486e-aba5-f9c296a2ae57", null, "user", "user" },
                    { "41f3e466-0891-4657-bbdf-93c51c784c62", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "187dea59-9896-486e-aba5-f9c296a2ae57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41f3e466-0891-4657-bbdf-93c51c784c62");

            migrationBuilder.DropColumn(
                name: "FaIcon",
                table: "category");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11a9bf1c-157b-4f73-98cb-2ff71fd24783", null, "admin", "admin" },
                    { "b2f6a931-f961-4986-95fe-c68f53f671af", null, "user", "user" }
                });
        }
    }
}
