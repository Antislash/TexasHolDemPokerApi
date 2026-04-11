using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentPlayerIdToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_CurrentPlayerId",
                table: "Room",
                column: "CurrentPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Player_CurrentPlayerId",
                table: "Room",
                column: "CurrentPlayerId",
                principalTable: "Player",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Player_CurrentPlayerId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_CurrentPlayerId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "Room");
        }
    }
}
