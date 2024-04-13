using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRepositories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameSessionMembers");

            migrationBuilder.DropColumn(
                name: "TeamRole",
                table: "GameSessionMembers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "GameSessions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<byte>(
                name: "Team",
                table: "GameRoundSelectorSelections",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "RoundNumber",
                table: "GameRounds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GameRoundOpposingTeamSelections",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "Team",
                table: "GameRoundOpposingTeamGuesses",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Team",
                table: "GameRoundGhostGuesses",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "GameSessionMemberRoundRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    Team = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessionMemberRoundRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSessionMemberRoundRoles_GameRounds_GameRoundId",
                        column: x => x.GameRoundId,
                        principalTable: "GameRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionMemberRoundRoles_GameRoundId",
                table: "GameSessionMemberRoundRoles",
                column: "GameRoundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessionMemberRoundRoles");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "GameRoundSelectorSelections");

            migrationBuilder.DropColumn(
                name: "RoundNumber",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "GameRoundGhostGuesses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "GameSessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GameSessionMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TeamRole",
                table: "GameSessionMembers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
