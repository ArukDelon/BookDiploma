using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class BookSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02af4303-15b3-4af7-a1d9-eaf9d5d14ea8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6448219e-e43a-4df0-8073-44986e1daf28");

            migrationBuilder.AddColumn<int>(
                name: "BookSeriesId",
                table: "books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookSeries_authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53760826-c0e5-499c-b2fd-081ec16941a7", null, "user", "user" },
                    { "dfa608ee-5a05-4fa9-843b-95c1416c544f", null, "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_BookSeriesId",
                table: "books",
                column: "BookSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookSeries_AuthorsId",
                table: "BookSeries",
                column: "AuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_BookSeries_BookSeriesId",
                table: "books",
                column: "BookSeriesId",
                principalTable: "BookSeries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_BookSeries_BookSeriesId",
                table: "books");

            migrationBuilder.DropTable(
                name: "BookSeries");

            migrationBuilder.DropIndex(
                name: "IX_books_BookSeriesId",
                table: "books");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53760826-c0e5-499c-b2fd-081ec16941a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa608ee-5a05-4fa9-843b-95c1416c544f");

            migrationBuilder.DropColumn(
                name: "BookSeriesId",
                table: "books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02af4303-15b3-4af7-a1d9-eaf9d5d14ea8", null, "user", "user" },
                    { "6448219e-e43a-4df0-8073-44986e1daf28", null, "admin", "admin" }
                });
        }
    }
}
