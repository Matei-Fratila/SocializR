using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocializR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameExtremecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfIncorrectExtremeQuestions",
                table: "GameSessions",
                newName: "NumberOfIncorrectExpertQuestions");

            migrationBuilder.RenameColumn(
                name: "NumberOfCorrectExtremeQuestions",
                table: "GameSessions",
                newName: "NumberOfCorrectExpertQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfIncorrectExpertQuestions",
                table: "GameSessions",
                newName: "NumberOfIncorrectExtremeQuestions");

            migrationBuilder.RenameColumn(
                name: "NumberOfCorrectExpertQuestions",
                table: "GameSessions",
                newName: "NumberOfCorrectExtremeQuestions");
        }
    }
}
