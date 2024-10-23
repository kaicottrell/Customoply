using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomMonopoly.Server.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardSquares_Games_GameId",
                table: "BoardSquares");

            migrationBuilder.DropIndex(
                name: "IX_BoardSquares_GameId",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "NumberOfRailroads",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "IsRailRoad",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "MorgageValue",
                table: "BoardSquares");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BoardSquares");

            migrationBuilder.RenameColumn(
                name: "Rent",
                table: "BoardSquares",
                newName: "TaxCost");

            migrationBuilder.AlterColumn<int>(
                name: "HouseCount",
                table: "PlayerProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HotelCount",
                table: "PlayerProperties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Boards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RailRoadMappingSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RentCost = table.Column<int>(type: "int", nullable: false),
                    NumberOfRailRoadsOwned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailRoadMappingSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRailRoadMappingSettings",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    RailRoadMappingSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRailRoadMappingSettings", x => new { x.GameId, x.RailRoadMappingSettingId });
                    table.ForeignKey(
                        name: "FK_GameRailRoadMappingSettings_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRailRoadMappingSettings_RailRoadMappingSettings_RailRoadMappingSettingId",
                        column: x => x.RailRoadMappingSettingId,
                        principalTable: "RailRoadMappingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_BoardId",
                table: "Games",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRailRoadMappingSettings_RailRoadMappingSettingId",
                table: "GameRailRoadMappingSettings",
                column: "RailRoadMappingSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Boards_BoardId",
                table: "Games",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Boards_BoardId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameRailRoadMappingSettings");

            migrationBuilder.DropTable(
                name: "RailRoadMappingSettings");

            migrationBuilder.DropIndex(
                name: "IX_Games_BoardId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TaxCost",
                table: "BoardSquares",
                newName: "Rent");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRailroads",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HouseCount",
                table: "PlayerProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelCount",
                table: "PlayerProperties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BoardSquares",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "BoardSquares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "BoardSquares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRailRoad",
                table: "BoardSquares",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MorgageValue",
                table: "BoardSquares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BoardSquares",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Boards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardSquares_GameId",
                table: "BoardSquares",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardSquares_Games_GameId",
                table: "BoardSquares",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
