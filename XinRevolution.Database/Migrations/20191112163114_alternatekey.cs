using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class alternatekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BlogTags_Id_BlogId_TagId",
                table: "BlogTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTags");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BlogTags_BlogId_TagId",
                table: "BlogTags",
                columns: new[] { "BlogId", "TagId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BlogTags_BlogId_TagId",
                table: "BlogTags");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BlogTags_Id_BlogId_TagId",
                table: "BlogTags",
                columns: new[] { "Id", "BlogId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogId",
                table: "BlogTags",
                column: "BlogId");
        }
    }
}
