using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class updateblogpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaReferenceContent",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "TextReferenceContent",
                table: "BlogPosts",
                newName: "ReferenceContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceContent",
                table: "BlogPosts",
                newName: "TextReferenceContent");

            migrationBuilder.AddColumn<string>(
                name: "MediaReferenceContent",
                table: "BlogPosts",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "");
        }
    }
}
