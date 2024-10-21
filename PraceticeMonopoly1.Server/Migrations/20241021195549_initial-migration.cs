using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomMonopoly.Server.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChanceCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyDifference = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChanceCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityChestCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyDifference = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityChestCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardChanceCards",
                columns: table => new
                {
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    ChanceCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardChanceCards", x => new { x.BoardId, x.ChanceCardId });
                    table.ForeignKey(
                        name: "FK_BoardChanceCards_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardChanceCards_ChanceCards_ChanceCardId",
                        column: x => x.ChanceCardId,
                        principalTable: "ChanceCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardCommunityChestCards",
                columns: table => new
                {
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    CommunityChestCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCommunityChestCards", x => new { x.BoardId, x.CommunityChestCardId });
                    table.ForeignKey(
                        name: "FK_BoardCommunityChestCards_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardCommunityChestCards_CommunityChestCards_CommunityChestCardId",
                        column: x => x.CommunityChestCardId,
                        principalTable: "CommunityChestCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardSquares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    CashPrize = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rent = table.Column<int>(type: "int", nullable: true),
                    MorgageValue = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: true),
                    IsRailRoad = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardSquares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardSquares_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    TurnsInJail = table.Column<int>(type: "int", nullable: true),
                    CurrentPostion = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    NumberOfRailroads = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardBoardSquares",
                columns: table => new
                {
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    BoardSquareId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardBoardSquares", x => new { x.BoardId, x.BoardSquareId });
                    table.ForeignKey(
                        name: "FK_BoardBoardSquares_BoardSquares_BoardSquareId",
                        column: x => x.BoardSquareId,
                        principalTable: "BoardSquares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardBoardSquares_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerProperties",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PropertySquareId = table.Column<int>(type: "int", nullable: false),
                    HouseCount = table.Column<int>(type: "int", nullable: false),
                    HotelCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProperties", x => new { x.PlayerId, x.PropertySquareId });
                    table.ForeignKey(
                        name: "FK_PlayerProperties_BoardSquares_PropertySquareId",
                        column: x => x.PropertySquareId,
                        principalTable: "BoardSquares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerProperties_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardBoardSquares_BoardSquareId",
                table: "BoardBoardSquares",
                column: "BoardSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardChanceCards_ChanceCardId",
                table: "BoardChanceCards",
                column: "ChanceCardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCommunityChestCards_CommunityChestCardId",
                table: "BoardCommunityChestCards",
                column: "CommunityChestCardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardSquares_GameId",
                table: "BoardSquares",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerProperties_PropertySquareId",
                table: "PlayerProperties",
                column: "PropertySquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "BoardBoardSquares");

            migrationBuilder.DropTable(
                name: "BoardChanceCards");

            migrationBuilder.DropTable(
                name: "BoardCommunityChestCards");

            migrationBuilder.DropTable(
                name: "PlayerProperties");

            migrationBuilder.DropTable(
                name: "ChanceCards");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "CommunityChestCards");

            migrationBuilder.DropTable(
                name: "BoardSquares");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
