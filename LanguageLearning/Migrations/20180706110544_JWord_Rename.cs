using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageLearning.Migrations
{
    public partial class JWord_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWord");

            migrationBuilder.CreateTable(
                name: "JapaneseWord",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    PartsOfSpeech = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JapaneseWord", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JapaneseWord");

            migrationBuilder.CreateTable(
                name: "JWord",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    PartsOfSpeech = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWord", x => x.ID);
                });
        }
    }
}
