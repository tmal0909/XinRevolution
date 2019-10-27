using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.UniqueConstraint("AK_Blogs_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DumpResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    DumpStatus = table.Column<bool>(type: "bit", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DumpResources", x => x.Id);
                    table.UniqueConstraint("AK_DumpResources_ResourceUrl", x => x.ResourceUrl);
                });

            migrationBuilder.CreateTable(
                name: "FGGroups",
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
                    table.PrimaryKey("PK_FGGroups", x => x.Id);
                    table.UniqueConstraint("AK_FGGroups_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.UniqueConstraint("AK_Issues_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.UniqueConstraint("AK_Tags_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Account", x => x.Account);
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
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.UniqueConstraint("AK_Works_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceType = table.Column<short>(type: "smallint", nullable: false),
                    ReferenceContent = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Sort = table.Column<short>(type: "smallint", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.UniqueConstraint("AK_BlogPosts_Id_BlogId", x => new { x.Id, x.BlogId });
                    table.ForeignKey(
                        name: "FK_BlogPosts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FGGroupRoles",
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
                    table.PrimaryKey("PK_FGGroupRoles", x => x.Id);
                    table.UniqueConstraint("AK_FGGroupRoles_Id_GroupId", x => new { x.Id, x.GroupId });
                    table.ForeignKey(
                        name: "FK_FGGroupRoles_FGGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FGGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueItems", x => x.Id);
                    table.UniqueConstraint("AK_IssueItems_Id_IssueId", x => new { x.Id, x.IssueId });
                    table.ForeignKey(
                        name: "FK_IssueItems_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueRelativeLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LinkUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    ResourceUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueRelativeLinks", x => x.Id);
                    table.UniqueConstraint("AK_IssueRelativeLinks_Id_IssueId", x => new { x.Id, x.IssueId });
                    table.ForeignKey(
                        name: "FK_IssueRelativeLinks_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    UtcUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                    table.UniqueConstraint("AK_BlogTags_Id_BlogId_TagId", x => new { x.Id, x.BlogId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BlogTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FGRoleEquipments",
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
                    table.PrimaryKey("PK_FGRoleEquipments", x => x.Id);
                    table.UniqueConstraint("AK_FGRoleEquipments_Id_RoleId", x => new { x.Id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_FGRoleEquipments_FGGroupRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "FGGroupRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Address", "Mail", "Name", "Password", "Phone" },
                values: new object[] { 1, "dev", "12345678", "dev@mail.com", "developer", "dev", "12345678" });

            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "Id", "Controller", "Intro", "Name", "ResourceUrl", "Sort" },
                values: new object[] { 1, "FireGeneration", "焰世代作品簡介", "焰世代", "default", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogId",
                table: "BlogPosts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_TagId",
                table: "BlogTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_FGGroupRoles_GroupId",
                table: "FGGroupRoles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FGRoleEquipments_RoleId",
                table: "FGRoleEquipments",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueItems_IssueId",
                table: "IssueItems",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueRelativeLinks_IssueId",
                table: "IssueRelativeLinks",
                column: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "DumpResources");

            migrationBuilder.DropTable(
                name: "FGRoleEquipments");

            migrationBuilder.DropTable(
                name: "IssueItems");

            migrationBuilder.DropTable(
                name: "IssueRelativeLinks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "FGGroupRoles");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "FGGroups");
        }
    }
}
