using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class BookSeries_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_BookSeries_BookSeriesId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_BookSeries_authors_AuthorsId",
                table: "BookSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookSeries",
                table: "BookSeries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53760826-c0e5-499c-b2fd-081ec16941a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa608ee-5a05-4fa9-843b-95c1416c544f");

            migrationBuilder.RenameTable(
                name: "BookSeries",
                newName: "bookSeries");

            migrationBuilder.RenameIndex(
                name: "IX_BookSeries_AuthorsId",
                table: "bookSeries",
                newName: "IX_bookSeries_AuthorsId");

            migrationBuilder.AddColumn<int>(
                name: "BookNumInSeries",
                table: "books",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookSeries",
                table: "bookSeries",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c13556b-fcb7-4dc8-994a-1b73ee2aefc2", null, "user", "user" },
                    { "bdce8e16-8a68-4d9a-80a6-94732e447786", null, "admin", "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_books_bookSeries_BookSeriesId",
                table: "books",
                column: "BookSeriesId",
                principalTable: "bookSeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookSeries_authors_AuthorsId",
                table: "bookSeries",
                column: "AuthorsId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_bookSeries_BookSeriesId",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_bookSeries_authors_AuthorsId",
                table: "bookSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookSeries",
                table: "bookSeries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c13556b-fcb7-4dc8-994a-1b73ee2aefc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdce8e16-8a68-4d9a-80a6-94732e447786");

            migrationBuilder.DropColumn(
                name: "BookNumInSeries",
                table: "books");

            migrationBuilder.RenameTable(
                name: "bookSeries",
                newName: "BookSeries");

            migrationBuilder.RenameIndex(
                name: "IX_bookSeries_AuthorsId",
                table: "BookSeries",
                newName: "IX_BookSeries_AuthorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookSeries",
                table: "BookSeries",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53760826-c0e5-499c-b2fd-081ec16941a7", null, "user", "user" },
                    { "dfa608ee-5a05-4fa9-843b-95c1416c544f", null, "admin", "admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_books_BookSeries_BookSeriesId",
                table: "books",
                column: "BookSeriesId",
                principalTable: "BookSeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSeries_authors_AuthorsId",
                table: "BookSeries",
                column: "AuthorsId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
