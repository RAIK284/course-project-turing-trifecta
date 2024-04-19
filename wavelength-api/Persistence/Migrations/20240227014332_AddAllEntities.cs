using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameRoundGhostGuesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundGhostGuesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundOpposingTeamGuesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsLeft = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundOpposingTeamGuesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundOpposingTeamSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsLeft = table.Column<bool>(type: "INTEGER", nullable: false),
                    Team = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundOpposingTeamSelections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundSelectorSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundSelectorSelections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameSessionResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WinningTeam = table.Column<byte>(type: "INTEGER", nullable: false),
                    WinningScore = table.Column<int>(type: "INTEGER", nullable: false),
                    LosingTeam = table.Column<byte>(type: "INTEGER", nullable: false),
                    LosingScore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessionResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpectrumCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LeftName = table.Column<string>(type: "TEXT", nullable: false),
                    RightName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpectrumCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TeamTurn = table.Column<byte>(type: "INTEGER", nullable: false),
                    SpectrumCardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Clue = table.Column<string>(type: "TEXT", nullable: false),
                    TargetOffset = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRounds_SpectrumCards_SpectrumCardId",
                        column: x => x.SpectrumCardId,
                        principalTable: "SpectrumCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_SpectrumCardId",
                table: "GameRounds",
                column: "SpectrumCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameRoundGhostGuesses");

            migrationBuilder.DropTable(
                name: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropTable(
                name: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropTable(
                name: "GameRounds");

            migrationBuilder.DropTable(
                name: "GameRoundSelectorSelections");

            migrationBuilder.DropTable(
                name: "GameSessionResults");

            migrationBuilder.DropTable(
                name: "SpectrumCards");
        }
    }
}
