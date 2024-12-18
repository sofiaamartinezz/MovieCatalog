using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Movies",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Movies",
                newName: "Description");
        }
    }
}
