using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDealerAndStack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDealer",
                table: "RoomPlayer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Stack",
                table: "RoomPlayer",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDealer",
                table: "RoomPlayer");

            migrationBuilder.DropColumn(
                name: "Stack",
                table: "RoomPlayer");
        }
    }
}
