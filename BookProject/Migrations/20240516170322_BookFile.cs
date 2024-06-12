using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class BookFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15130d9e-38ab-4728-a508-58d2d74b1b48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61cdb6e3-30ed-4c59-b33d-e36b7e680d24");

            migrationBuilder.AddColumn<string>(
                name: "BookFilePath",
                table: "books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02af4303-15b3-4af7-a1d9-eaf9d5d14ea8", null, "user", "user" },
                    { "6448219e-e43a-4df0-8073-44986e1daf28", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02af4303-15b3-4af7-a1d9-eaf9d5d14ea8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6448219e-e43a-4df0-8073-44986e1daf28");

            migrationBuilder.DropColumn(
                name: "BookFilePath",
                table: "books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15130d9e-38ab-4728-a508-58d2d74b1b48", null, "admin", "admin" },
                    { "61cdb6e3-30ed-4c59-b33d-e36b7e680d24", null, "user", "user" }
                });
        }
    }
}
