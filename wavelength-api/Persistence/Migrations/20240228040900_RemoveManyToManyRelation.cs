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
                name: "FK_GameSessionMembers_AspNetUsers_UserID",
                table: "GameSessionMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "GameSessionMembers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionResults_GameSessionID",
                table: "GameSessionResults",
                column: "GameSessionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundSelectorSelections_GameRoundID",
                table: "GameRoundSelectorSelections",
                column: "GameRoundID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundOpposingTeamSelections_GameRoundID",
                table: "GameRoundOpposingTeamSelections",
                column: "GameRoundID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundOpposingTeamGuesses_GameRoundID",
                table: "GameRoundOpposingTeamGuesses",
                column: "GameRoundID");

            migrationBuilder.CreateIndex(
                name: "IX_GameRoundGhostGuesses_GameRoundID",
                table: "GameRoundGhostGuesses",
                column: "GameRoundID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundGhostGuesses_GameRounds_GameRoundID",
                table: "GameRoundGhostGuesses",
                column: "GameRoundID",
                principalTable: "GameRounds",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundOpposingTeamGuesses_GameRounds_GameRoundID",
                table: "GameRoundOpposingTeamGuesses",
                column: "GameRoundID",
                principalTable: "GameRounds",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundOpposingTeamSelections_GameRounds_GameRoundID",
                table: "GameRoundOpposingTeamSelections",
                column: "GameRoundID",
                principalTable: "GameRounds",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRoundSelectorSelections_GameRounds_GameRoundID",
                table: "GameRoundSelectorSelections",
                column: "GameRoundID",
                principalTable: "GameRounds",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionResults_GameSessions_GameSessionID",
                table: "GameSessionResults",
                column: "GameSessionID",
                principalTable: "GameSessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundGhostGuesses_GameRounds_GameRoundID",
                table: "GameRoundGhostGuesses");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundOpposingTeamGuesses_GameRounds_GameRoundID",
                table: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundOpposingTeamSelections_GameRounds_GameRoundID",
                table: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRoundSelectorSelections_GameRounds_GameRoundID",
                table: "GameRoundSelectorSelections");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionResults_GameSessions_GameSessionID",
                table: "GameSessionResults");

            migrationBuilder.DropIndex(
                name: "IX_GameSessionResults_GameSessionID",
                table: "GameSessionResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundSelectorSelections_GameRoundID",
                table: "GameRoundSelectorSelections");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundOpposingTeamSelections_GameRoundID",
                table: "GameRoundOpposingTeamSelections");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundOpposingTeamGuesses_GameRoundID",
                table: "GameRoundOpposingTeamGuesses");

            migrationBuilder.DropIndex(
                name: "IX_GameRoundGhostGuesses_GameRoundID",
                table: "GameRoundGhostGuesses");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "GameSessionMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionMembers",
                table: "GameSessionMembers",
                columns: new[] { "UserID", "GameSessionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionMembers_AspNetUsers_UserID",
                table: "GameSessionMembers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
