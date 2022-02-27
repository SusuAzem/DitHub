using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Data.Migrations
{
    public partial class FaveDitsF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaveDits",
                columns: table => new
                {
                    DitId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaveDits", x => new { x.DitId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_FaveDits_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaveDits_Dits_DitId",
                        column: x => x.DitId,
                        principalTable: "Dits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaveDits_AppUserId",
                table: "FaveDits",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaveDits");
        }
    }
}
