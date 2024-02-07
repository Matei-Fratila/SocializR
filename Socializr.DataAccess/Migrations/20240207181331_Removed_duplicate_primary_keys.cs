using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocializR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Removed_duplicate_primary_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_RequesterUserId",
                table: "FriendRequests",
                column: "RequesterUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_RequesterUserId",
                table: "FriendRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                columns: new[] { "RequesterUserId", "RequestedUserId" });
        }
    }
}
