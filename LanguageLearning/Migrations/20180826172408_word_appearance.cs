using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageLearning.Migrations
{
    public partial class word_appearance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordAppearanceSessions");

            migrationBuilder.CreateTable(
                name: "HiraganaAppearances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    HiraganaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiraganaAppearances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HiraganaAppearances_Hiragana_HiraganaID",
                        column: x => x.HiraganaID,
                        principalTable: "Hiragana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HiraganaAppearances_UserData_UserID",
                        column: x => x.UserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JapaneseWordAppearances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    JapaneseWordID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JapaneseWordAppearances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JapaneseWordAppearances_JapaneseWord_JapaneseWordID",
                        column: x => x.JapaneseWordID,
                        principalTable: "JapaneseWord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JapaneseWordAppearances_UserData_UserID",
                        column: x => x.UserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KatakanaAppearances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    KatakanaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatakanaAppearances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KatakanaAppearances_Katakana_KatakanaID",
                        column: x => x.KatakanaID,
                        principalTable: "Katakana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KatakanaAppearances_UserData_UserID",
                        column: x => x.UserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KoreanWordAppearances",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    KoreanWordID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoreanWordAppearances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KoreanWordAppearances_KoreanWord_KoreanWordID",
                        column: x => x.KoreanWordID,
                        principalTable: "KoreanWord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KoreanWordAppearances_UserData_UserID",
                        column: x => x.UserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiraganaAppearances_HiraganaID",
                table: "HiraganaAppearances",
                column: "HiraganaID");

            migrationBuilder.CreateIndex(
                name: "IX_HiraganaAppearances_UserID",
                table: "HiraganaAppearances",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_JapaneseWordAppearances_JapaneseWordID",
                table: "JapaneseWordAppearances",
                column: "JapaneseWordID");

            migrationBuilder.CreateIndex(
                name: "IX_JapaneseWordAppearances_UserID",
                table: "JapaneseWordAppearances",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_KatakanaAppearances_KatakanaID",
                table: "KatakanaAppearances",
                column: "KatakanaID");

            migrationBuilder.CreateIndex(
                name: "IX_KatakanaAppearances_UserID",
                table: "KatakanaAppearances",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_KoreanWordAppearances_KoreanWordID",
                table: "KoreanWordAppearances",
                column: "KoreanWordID");

            migrationBuilder.CreateIndex(
                name: "IX_KoreanWordAppearances_UserID",
                table: "KoreanWordAppearances",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HiraganaAppearances");

            migrationBuilder.DropTable(
                name: "JapaneseWordAppearances");

            migrationBuilder.DropTable(
                name: "KatakanaAppearances");

            migrationBuilder.DropTable(
                name: "KoreanWordAppearances");

            migrationBuilder.CreateTable(
                name: "WordAppearanceSessions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HiraganaID = table.Column<int>(nullable: true),
                    JapaneseWordID = table.Column<int>(nullable: true),
                    KatakanaID = table.Column<int>(nullable: true),
                    KoreanWordID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAppearanceSessions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WordAppearanceSessions_Hiragana_HiraganaID",
                        column: x => x.HiraganaID,
                        principalTable: "Hiragana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordAppearanceSessions_JapaneseWord_JapaneseWordID",
                        column: x => x.JapaneseWordID,
                        principalTable: "JapaneseWord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordAppearanceSessions_Katakana_KatakanaID",
                        column: x => x.KatakanaID,
                        principalTable: "Katakana",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordAppearanceSessions_KoreanWord_KoreanWordID",
                        column: x => x.KoreanWordID,
                        principalTable: "KoreanWord",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordAppearanceSessions_UserData_UserID",
                        column: x => x.UserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordAppearanceSessions_HiraganaID",
                table: "WordAppearanceSessions",
                column: "HiraganaID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAppearanceSessions_JapaneseWordID",
                table: "WordAppearanceSessions",
                column: "JapaneseWordID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAppearanceSessions_KatakanaID",
                table: "WordAppearanceSessions",
                column: "KatakanaID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAppearanceSessions_KoreanWordID",
                table: "WordAppearanceSessions",
                column: "KoreanWordID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAppearanceSessions_UserID",
                table: "WordAppearanceSessions",
                column: "UserID");
        }
    }
}
