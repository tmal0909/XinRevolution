using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "Works",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IssueRelativeLinks",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IssueItems",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "ViceResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "MainResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "FGGroups",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "RelativeLinkUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "CoverViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "CoverMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "CharacterMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "Works",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IssueRelativeLinks",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IssueItems",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ViceResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "FGGroups",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RelativeLinkUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoverViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CoverMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharacterViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CharacterMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);
        }
    }
}
