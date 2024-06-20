using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp_Task.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_userQuizzes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QuizId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QuizCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_userQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_userQuizzes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_userQuizzes_tbl_quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "tbl_quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_userAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserQuizId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QuestionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AnswerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_userAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_userAnswers_tbl_answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "tbl_answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_userAnswers_tbl_question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tbl_question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_userAnswers_tbl_userQuizzes_UserQuizId",
                        column: x => x.UserQuizId,
                        principalTable: "tbl_userQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userAnswers_AnswerId",
                table: "tbl_userAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userAnswers_QuestionId",
                table: "tbl_userAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userAnswers_UserQuizId",
                table: "tbl_userAnswers",
                column: "UserQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userQuizzes_QuizId",
                table: "tbl_userQuizzes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_userQuizzes_UserId",
                table: "tbl_userQuizzes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_userAnswers");

            migrationBuilder.DropTable(
                name: "tbl_userQuizzes");
        }
    }
}
