using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class FireGenerationRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FGGroupEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BackgroundResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGGroupEntity", x => x.Id);
                    table.UniqueConstraint("AK_FGGroupEntity_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.UniqueConstraint("AK_Works_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FGGroupRoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CoverMainResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    CoverViceResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    CharacterMainResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    CharacterViceResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    RelativeLinkUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Sort = table.Column<short>(type: "smallint", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGGroupRoleEntity", x => x.Id);
                    table.UniqueConstraint("AK_FGGroupRoleEntity_Id_GroupId", x => new { x.Id, x.GroupId });
                    table.ForeignKey(
                        name: "FK_FGGroupRoleEntity_FGGroupEntity_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FGGroupEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FGRoleEquipmentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    MainResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    ViceResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FGRoleEquipmentEntity", x => x.Id);
                    table.UniqueConstraint("AK_FGRoleEquipmentEntity_Id_RoleId", x => new { x.Id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_FGRoleEquipmentEntity_FGGroupRoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "FGGroupRoleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FGGroupRoleEntity_GroupId",
                table: "FGGroupRoleEntity",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FGRoleEquipmentEntity_RoleId",
                table: "FGRoleEquipmentEntity",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FGRoleEquipmentEntity");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "FGGroupRoleEntity");

            migrationBuilder.DropTable(
                name: "FGGroupEntity");
        }
    }
}
