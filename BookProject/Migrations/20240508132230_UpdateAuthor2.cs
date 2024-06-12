using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f226856-44dc-43de-ad44-370d5458371a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "affeb9ed-1a79-48c5-a6df-f1c028048be3");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a6bd8f1-fdee-48e9-a10e-2e1af60d3685", null, "admin", "admin" },
                    { "381743e5-1a48-4922-94c6-2402272032bd", null, "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a6bd8f1-fdee-48e9-a10e-2e1af60d3685");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "381743e5-1a48-4922-94c6-2402272032bd");

            migrationBuilder.DropColumn(
                name: "BirthDate",
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
    }
}
