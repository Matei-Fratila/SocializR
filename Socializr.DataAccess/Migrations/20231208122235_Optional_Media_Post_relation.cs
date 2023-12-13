using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocializR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Optional_Media_Post_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Media",
                newName: "FileName");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Media",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Media",
                newName: "FilePath");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Media",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
