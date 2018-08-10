using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageLearning.Migrations
{
    public partial class authentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "UserData",
                newName: "StringifiedSalt");

            migrationBuilder.AlterColumn<string>(
                name: "Romaji",
                table: "Katakana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kana",
                table: "Katakana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Romaji",
                table: "Hiragana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Kana",
                table: "Hiragana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StringifiedSalt",
                table: "UserData",
                newName: "Salt");

            migrationBuilder.AlterColumn<string>(
                name: "Romaji",
                table: "Katakana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Kana",
                table: "Katakana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Romaji",
                table: "Hiragana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Kana",
                table: "Hiragana",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
