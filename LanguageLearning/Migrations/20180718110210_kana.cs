using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageLearning.Migrations
{
    public partial class kana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hiragana",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Kana = table.Column<string>(nullable: true),
                    Romaji = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hiragana", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Katakana",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Kana = table.Column<string>(nullable: true),
                    Romaji = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Katakana", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hiragana");

            migrationBuilder.DropTable(
                name: "Katakana");
        }
    }
}
