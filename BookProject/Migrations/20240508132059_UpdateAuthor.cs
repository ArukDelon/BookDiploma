using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39b03058-95e6-4812-ba1d-5478ab9ce6f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b12f0643-7d78-41b9-9684-b8fd6c8dd6ea");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "authors");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f226856-44dc-43de-ad44-370d5458371a", null, "admin", "admin" },
                    { "affeb9ed-1a79-48c5-a6df-f1c028048be3", null, "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f226856-44dc-43de-ad44-370d5458371a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "affeb9ed-1a79-48c5-a6df-f1c028048be3");

            migrationBuilder.AddColumn<string>(
                name: "BirthDate",
                table: "authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeathDate",
                table: "authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39b03058-95e6-4812-ba1d-5478ab9ce6f0", null, "user", "user" },
                    { "b12f0643-7d78-41b9-9684-b8fd6c8dd6ea", null, "admin", "admin" }
                });
        }
    }
}
