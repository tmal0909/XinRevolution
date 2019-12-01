using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class FGChapterComicChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "FGChapterComics");

            migrationBuilder.AddColumn<short>(
                name: "Page",
                table: "FGChapterComics",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGSeasonChapters_SerialNumber",
                table: "FGSeasonChapters",
                column: "SerialNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGChapterComics_Page",
                table: "FGChapterComics",
                column: "Page");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGSeasonChapters_SerialNumber",
                table: "FGSeasonChapters");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGChapterComics_Page",
                table: "FGChapterComics");

            migrationBuilder.DropColumn(
                name: "Page",
                table: "FGChapterComics");

            migrationBuilder.AddColumn<short>(
                name: "Sort",
                table: "FGChapterComics",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
