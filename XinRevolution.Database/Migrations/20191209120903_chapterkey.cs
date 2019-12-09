using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class chapterkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGSeasonChapters_SerialNumber",
                table: "FGSeasonChapters");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGChapterComics_Page",
                table: "FGChapterComics");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGSeasonChapters_Id_SerialNumber",
                table: "FGSeasonChapters",
                columns: new[] { "Id", "SerialNumber" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGChapterComics_Id_Page",
                table: "FGChapterComics",
                columns: new[] { "Id", "Page" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGSeasonChapters_Id_SerialNumber",
                table: "FGSeasonChapters");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FGChapterComics_Id_Page",
                table: "FGChapterComics");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGSeasonChapters_SerialNumber",
                table: "FGSeasonChapters",
                column: "SerialNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FGChapterComics_Page",
                table: "FGChapterComics",
                column: "Page");
        }
    }
}
