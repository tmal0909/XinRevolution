using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class FGGroupRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "FGRoleEquipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Intro",
                table: "FGGroupRoles",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "FGRoleEquipments");

            migrationBuilder.DropColumn(
                name: "Intro",
                table: "FGGroupRoles");
        }
    }
}
