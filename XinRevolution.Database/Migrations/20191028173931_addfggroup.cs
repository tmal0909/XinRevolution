using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class addfggroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BackgroundResourceUrl",
                table: "FGGroups",
                newName: "ResourceUrl");

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "FGGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "FGGroups");

            migrationBuilder.RenameColumn(
                name: "ResourceUrl",
                table: "FGGroups",
                newName: "BackgroundResourceUrl");
        }
    }
}
