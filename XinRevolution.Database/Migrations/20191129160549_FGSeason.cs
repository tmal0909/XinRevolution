using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class FGSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FGSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGSeasons", x => x.Id);
                    table.UniqueConstraint("AK_FGSeasons_SerialNumber", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "FGSeasonChapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Offset = table.Column<int>(type: "int", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGSeasonChapters", x => x.Id);
                    table.UniqueConstraint("AK_FGSeasonChapters_Id_SeasonId", x => new { x.Id, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_FGSeasonChapters_FGSeasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "FGSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FGChapterComics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Sort = table.Column<short>(type: "smallint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGChapterComics", x => x.Id);
                    table.UniqueConstraint("AK_FGChapterComics_Id_ChapterId", x => new { x.Id, x.ChapterId });
                    table.ForeignKey(
                        name: "FK_FGChapterComics_FGSeasonChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "FGSeasonChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FGChapterComics_ChapterId",
                table: "FGChapterComics",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_FGSeasonChapters_SeasonId",
                table: "FGSeasonChapters",
                column: "SeasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FGChapterComics");

            migrationBuilder.DropTable(
                name: "FGSeasonChapters");

            migrationBuilder.DropTable(
                name: "FGSeasons");
        }
    }
}
