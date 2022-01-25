using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Data.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits");

            migrationBuilder.DropForeignKey(
                name: "FK_Dits_Genres_GenreId",
                table: "Dits");

            migrationBuilder.AlterColumn<byte>(
                name: "GenreId",
                table: "Dits",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Dits",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_Genres_GenreId",
                table: "Dits",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits");

            migrationBuilder.DropForeignKey(
                name: "FK_Dits_Genres_GenreId",
                table: "Dits");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte>(
                name: "GenreId",
                table: "Dits",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Dits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_AspNetUsers_ArtistId",
                table: "Dits",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dits_Genres_GenreId",
                table: "Dits",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
