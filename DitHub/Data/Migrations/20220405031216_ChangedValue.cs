using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Data.Migrations
{
    public partial class ChangedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "changed",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "changed",
                table: "Notifications");
        }
    }
}
