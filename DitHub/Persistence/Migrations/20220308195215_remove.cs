using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Persistence.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RemoveFlag",
                table: "Dits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemoveFlag",
                table: "Dits");
        }
    }
}
