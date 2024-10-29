using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomMonopoly.Server.Migrations
{
    /// <inheritdoc />
    public partial class addPlayerTurn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlayersTurn",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TurnOrder",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlayersTurn",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TurnOrder",
                table: "Players");
        }
    }
}
