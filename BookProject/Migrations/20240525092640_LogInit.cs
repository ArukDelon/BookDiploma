using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class LogInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4414d608-5d2d-4139-82c7-4a1da0fe14e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad5ce127-70e1-4b1a-8e56-5bb20b4a7e4a");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    AuthorsId = table.Column<int>(type: "int", nullable: true),
                    TagId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    BookSeriesId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_logs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logs_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_logs_authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_logs_bookSeries_BookSeriesId",
                        column: x => x.BookSeriesId,
                        principalTable: "bookSeries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_logs_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK_logs_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11a9bf1c-157b-4f73-98cb-2ff71fd24783", null, "admin", "admin" },
                    { "b2f6a931-f961-4986-95fe-c68f53f671af", null, "user", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_AppUserId",
                table: "logs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_AuthorsId",
                table: "logs",
                column: "AuthorsId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_BookId",
                table: "logs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_BookSeriesId",
                table: "logs",
                column: "BookSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_CategoryId",
                table: "logs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_TagId",
                table: "logs",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11a9bf1c-157b-4f73-98cb-2ff71fd24783");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f6a931-f961-4986-95fe-c68f53f671af");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4414d608-5d2d-4139-82c7-4a1da0fe14e5", null, "admin", "admin" },
                    { "ad5ce127-70e1-4b1a-8e56-5bb20b4a7e4a", null, "user", "user" }
                });
        }
    }
}
