using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LanguageLearning.Migrations
{
    public partial class JWords4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JAdjective");

            migrationBuilder.DropTable(
                name: "JVerb");
          
            migrationBuilder.CreateTable(
                name: "JapaneseAdjective",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TypeOfAdjective = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JapaneseAdjective", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JapaneseVerb",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TypeOfVerb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JapaneseVerb", x => x.ID);
                });           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JapaneseAdjective");

            migrationBuilder.DropTable(
                name: "JapaneseVerb");

            migrationBuilder.DropTable(
                name: "UserData");           

            migrationBuilder.CreateTable(
                name: "JAdjective",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TypeOfAdjective = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JAdjective", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JVerb",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Definition = table.Column<string>(nullable: true),
                    Kana = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TypeOfVerb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JVerb", x => x.ID);
                });
        }
    }
}
