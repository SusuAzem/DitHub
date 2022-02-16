using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Data.Migrations
{
    public partial class Appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Dits",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dits_ArtistId",
                table: "Dits",
                newName: "IX_Dits_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_AspNetUsers_AppUserId",
                table: "Dits",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dits_AspNetUsers_AppUserId",
                table: "Dits");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Dits",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Dits_AppUserId",
                table: "Dits",
                newName: "IX_Dits_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
