using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TexasHolDemPokerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerLoginRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "PassWord",
                table: "Player",
                newName: "Pseudo");

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_LoginId",
                table: "Player",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Login_LoginId",
                table: "Player",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Login_LoginId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_LoginId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "Pseudo",
                table: "Player",
                newName: "PassWord");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Player",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
