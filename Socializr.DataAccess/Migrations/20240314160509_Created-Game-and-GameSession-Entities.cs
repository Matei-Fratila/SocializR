using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocializR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatedGameandGameSessionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Game_NumberOfHearts",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Game_XP",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameType = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectEasyQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectMediumQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectHardQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectExtremeQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfIncorrectEasyQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfIncorrectMediumQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfIncorrectHardQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfIncorrectExtremeQuestions = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSessions_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_UserId",
                table: "GameSessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessions");

            migrationBuilder.DropColumn(
                name: "Game_NumberOfHearts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Game_XP",
                table: "Users");
        }
    }
}
