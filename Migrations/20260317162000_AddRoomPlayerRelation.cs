using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomPlayerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomPlayer",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlayer", x => new { x.RoomId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_RoomPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomPlayer_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayer_PlayerId",
                table: "RoomPlayer",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPlayer");
        }
    }
}
