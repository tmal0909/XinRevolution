using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class fgview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FGViewCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGViewCategories", x => x.Id);
                    table.UniqueConstraint("AK_FGViewCategories_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FGViewCategoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGViewCategoryEvents", x => x.Id);
                    table.UniqueConstraint("AK_FGViewCategoryEvents_Id_CategoryId", x => new { x.Id, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_FGViewCategoryEvents_FGViewCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FGViewCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FGViewCategoryEvents_CategoryId",
                table: "FGViewCategoryEvents",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FGViewCategoryEvents");

            migrationBuilder.DropTable(
                name: "FGViewCategories");
        }
    }
}
