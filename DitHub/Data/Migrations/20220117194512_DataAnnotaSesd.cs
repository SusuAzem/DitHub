using Microsoft.EntityFrameworkCore.Migrations;

namespace DitHub.Data.Migrations
{
    public partial class DataAnnotaSesd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Venue",
                table: "Dits",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ArtistId",
            //    table: "Dits",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "GenreId",
            //    table: "Dits",
            //    type: "tinyint",
            //    nullable: false,
            //    defaultValue: "",
            //    oldNullable: true);

            migrationBuilder.Sql("INSERT INTO Genres (Id , Name) VALUES (1, 'Jazz') ");
            migrationBuilder.Sql("INSERT INTO Genres (Id , Name) VALUES (2, 'Blues') ");
            migrationBuilder.Sql("INSERT INTO Genres (Id , Name) VALUES (3, 'Rock') ");
            migrationBuilder.Sql("INSERT INTO Genres (Id , Name) VALUES (4, 'Country') ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Venue",
                table: "Dits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ArtistId",
            //    table: "Dits",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //   name: "GenreId",
            //   table: "Dits",
            //   type: "tinyint",
            //   nullable: true);

            migrationBuilder.Sql("DELETE FROM Genres WHERE Id IN (1,2,3,4) ");

        }
    }
}
