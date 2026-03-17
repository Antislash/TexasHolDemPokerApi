using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Room");
        }
    }
}
