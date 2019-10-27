using Microsoft.EntityFrameworkCore.Migrations;

namespace XinRevolution.Database.Migrations
{
    public partial class seeddataofwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "Id", "Controller", "Intro", "Name", "ResourceUrl" },
                values: new object[] { 1, "FireGeneration", "焰世代作品簡介", "焰世代", "default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Works",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
