using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDealerPlayerIdToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealerPlayerId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_DealerPlayerId",
                table: "Room",
                column: "DealerPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Player_DealerPlayerId",
                table: "Room",
                column: "DealerPlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Player_DealerPlayerId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_DealerPlayerId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "DealerPlayerId",
                table: "Room");
        }
    }
}
