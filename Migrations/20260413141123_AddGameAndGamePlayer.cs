using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAndGamePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Player_CurrentPlayerId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Player_DealerPlayerId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_CurrentPlayerId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_DealerPlayerId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "DealerPlayerId",
                table: "Room");

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    DealerPlayerId = table.Column<int>(type: "int", nullable: true),
                    CurrentPlayerId = table.Column<int>(type: "int", nullable: true),
                    SmallBlindAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BigBlindAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Player_CurrentPlayerId",
                        column: x => x.CurrentPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Game_Player_DealerPlayerId",
                        column: x => x.DealerPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Game_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayer",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Stack = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrentBet = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HasFolded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayer", x => new { x.GameId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_GamePlayer_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_CurrentPlayerId",
                table: "Game",
                column: "CurrentPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DealerPlayerId",
                table: "Game",
                column: "DealerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_RoomId",
                table: "Game",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_PlayerId",
                table: "GamePlayer",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayer");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DealerPlayerId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_CurrentPlayerId",
                table: "Room",
                column: "CurrentPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_DealerPlayerId",
                table: "Room",
                column: "DealerPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Player_CurrentPlayerId",
                table: "Room",
                column: "CurrentPlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Player_DealerPlayerId",
                table: "Room",
                column: "DealerPlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
