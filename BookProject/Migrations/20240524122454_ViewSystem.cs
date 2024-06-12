using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class ViewSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c13556b-fcb7-4dc8-994a-1b73ee2aefc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdce8e16-8a68-4d9a-80a6-94732e447786");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4414d608-5d2d-4139-82c7-4a1da0fe14e5", null, "admin", "admin" },
                    { "ad5ce127-70e1-4b1a-8e56-5bb20b4a7e4a", null, "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4414d608-5d2d-4139-82c7-4a1da0fe14e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad5ce127-70e1-4b1a-8e56-5bb20b4a7e4a");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c13556b-fcb7-4dc8-994a-1b73ee2aefc2", null, "user", "user" },
                    { "bdce8e16-8a68-4d9a-80a6-94732e447786", null, "admin", "admin" }
                });
        }
    }
}
