using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class RemoveKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_IssueRelativeLinks_ResourceUrl_IssueId",
                table: "IssueRelativeLinks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_IssueItems_Title_ReleaseDate_IssueId",
                table: "IssueItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_IssueRelativeLinks_ResourceUrl_IssueId",
                table: "IssueRelativeLinks",
                columns: new[] { "ResourceUrl", "IssueId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_IssueItems_Title_ReleaseDate_IssueId",
                table: "IssueItems",
                columns: new[] { "Title", "ReleaseDate", "IssueId" });
        }
    }
}
