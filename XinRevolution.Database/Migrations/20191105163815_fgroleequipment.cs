using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class fgroleequipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayResourceUrl",
                table: "FGRoleEquipments",
                newName: "SlideResourceUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SlideResourceUrl",
                table: "FGRoleEquipments",
                newName: "DisplayResourceUrl");
        }
    }
}
