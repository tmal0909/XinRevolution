using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class UpdateAlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_IssueRelativeLinks_Id_IssueId",
                table: "IssueRelativeLinks",
                columns: new[] { "Id", "IssueId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_IssueItems_Id_IssueId",
                table: "IssueItems",
                columns: new[] { "Id", "IssueId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_IssueRelativeLinks_Id_IssueId",
                table: "IssueRelativeLinks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_IssueItems_Id_IssueId",
                table: "IssueItems");
        }
    }
}
