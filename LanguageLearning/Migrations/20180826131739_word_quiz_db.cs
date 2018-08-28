using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageLearning.Migrations
{
    public partial class word_quiz_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizSessionModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentUserID = table.Column<int>(nullable: true),
                    CurrentLanguage = table.Column<int>(nullable: false),
                    WordLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSessionModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuizSessionModel_UserData_CurrentUserID",
                        column: x => x.CurrentUserID,
                        principalTable: "UserData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WordAppearanceSessions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: true),
                    JapaneseWordID = table.Column<int>(nullable: true),
                    HiraganaID = table.Column<int>(nullable: true),
                    KatakanaID = table.Column<int>(nullable: true),
                    KoreanWordID = table.Column<int>(nullable: true)
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
                name: "IX_QuizSessionModel_CurrentUserID",
                table: "QuizSessionModel",
                column: "CurrentUserID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizSessionModel");

            migrationBuilder.DropTable(
                name: "WordAppearanceSessions");
        }
    }
}
