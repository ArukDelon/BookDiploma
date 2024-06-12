using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class StarSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "951f841c-20bb-47f0-9bc4-d9c8427f0ced");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bed6bbde-8d5f-4496-b82f-7a21a2ee5d84");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageGrade",
                table: "books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalGrades",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15130d9e-38ab-4728-a508-58d2d74b1b48", null, "admin", "admin" },
                    { "61cdb6e3-30ed-4c59-b33d-e36b7e680d24", null, "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15130d9e-38ab-4728-a508-58d2d74b1b48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61cdb6e3-30ed-4c59-b33d-e36b7e680d24");

            migrationBuilder.DropColumn(
                name: "AverageGrade",
                table: "books");

            migrationBuilder.DropColumn(
                name: "TotalGrades",
                table: "books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "951f841c-20bb-47f0-9bc4-d9c8427f0ced", null, "admin", "admin" },
                    { "bed6bbde-8d5f-4496-b82f-7a21a2ee5d84", null, "user", "user" }
                });
        }
    }
}
