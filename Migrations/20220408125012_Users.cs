using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectPolicyQuiz.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserInfoId);
                });

            migrationBuilder.InsertData(
                table: "Quizs",
                columns: new[] { "QuizId", "QuizDate", "QuizPassNumber", "QuizPersonName", "QuizTitle" },
                values: new object[] { 1, new DateTime(2020, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, "Copyright" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserInfoId", "Password", "Username" },
                values: new object[] { 1, "1234_abc", "Anupama" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Quizs",
                keyColumn: "QuizId",
                keyValue: 1);
        }
    }
}
