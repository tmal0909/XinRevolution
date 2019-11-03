using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class fgroleresource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "FGRoleEquipments");

            migrationBuilder.DropColumn(
                name: "ViceResourceUrl",
                table: "FGRoleEquipments");

            migrationBuilder.DropColumn(
                name: "CharacterMainResourceUrl",
                table: "FGGroupRoles");

            migrationBuilder.DropColumn(
                name: "CharacterViceResourceUrl",
                table: "FGGroupRoles");

            migrationBuilder.DropColumn(
                name: "CoverMainResourceUrl",
                table: "FGGroupRoles");

            migrationBuilder.DropColumn(
                name: "CoverViceResourceUrl",
                table: "FGGroupRoles");

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
                name: "MainResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "Sort",
                table: "FGRoleEquipments",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "FGGroups",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FGRoleResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Sort = table.Column<short>(type: "smallint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGRoleResources", x => x.Id);
                    table.UniqueConstraint("AK_FGRoleResources_Id_RoleId", x => new { x.Id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_FGRoleResources_FGGroupRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "FGGroupRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FGRoleResources_RoleId",
                table: "FGRoleResources",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FGRoleResources");

            migrationBuilder.DropColumn(
                name: "DisplayResourceUrl",
                table: "FGRoleEquipments");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "FGRoleEquipments");

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
                name: "MainResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "FGRoleEquipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ViceResourceUrl",
                table: "FGRoleEquipments",
                type: "nvarchar(300)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "FGGroups",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AddColumn<string>(
                name: "CharacterMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverMainResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverViceResourceUrl",
                table: "FGGroupRoles",
                type: "nvarchar(300)",
                nullable: true);
        }
    }
}
