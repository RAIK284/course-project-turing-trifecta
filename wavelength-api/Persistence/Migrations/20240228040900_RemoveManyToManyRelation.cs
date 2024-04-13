using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionMembers_AspNetUsers_UserId",
                table: "GameSessionMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GameSessionMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionResults_GameSessionId",
                table: "GameSessionResults",
                column: "GameSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundSelectorSelections_GameRoundId",
                table: "GameRoundSelectorSelections",
                column: "GameRoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundOpposingTeamSelections_GameRoundId",
                table: "GameRoundOpposingTeamSelections",
                column: "GameRoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundOpposingTeamGuesses_GameRoundId",
                table: "GameRoundOpposingTeamGuesses",
                column: "GameRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundGhostGuesses_GameRoundId",
                table: "GameRoundGhostGuesses",
                column: "GameRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundGhostGuesses_GameRounds_GameRoundId",
                table: "GameRoundGhostGuesses",
                column: "GameRoundId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundOpposingTeamGuesses_GameRounds_GameRoundId",
                table: "GameRoundOpposingTeamGuesses",
                column: "GameRoundId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundOpposingTeamSelections_GameRounds_GameRoundId",
                table: "GameRoundOpposingTeamSelections",
                column: "GameRoundId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundSelectorSelections_GameRounds_GameRoundId",
                table: "GameRoundSelectorSelections",
                column: "GameRoundId",
                principalTable: "GameRounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionResults_GameSessions_GameSessionId",
                table: "GameSessionResults",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundGhostGuesses_GameRounds_GameRoundId",
                table: "GameRoundGhostGuesses");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundOpposingTeamGuesses_GameRounds_GameRoundId",
                table: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundOpposingTeamSelections_GameRounds_GameRoundId",
                table: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundSelectorSelections_GameRounds_GameRoundId",
                table: "GameRoundSelectorSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionResults_GameSessions_GameSessionId",
                table: "GameSessionResults");

            migrationBuilder.DropIndex(
                name: "IX_GameSessionResults_GameSessionId",
                table: "GameSessionResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundSelectorSelections_GameRoundId",
                table: "GameRoundSelectorSelections");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundOpposingTeamSelections_GameRoundId",
                table: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundOpposingTeamGuesses_GameRoundId",
                table: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundGhostGuesses_GameRoundId",
                table: "GameRoundGhostGuesses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameSessionMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers",
                columns: new[] { "UserId", "GameSessionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionMembers_AspNetUsers_UserId",
                table: "GameSessionMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
