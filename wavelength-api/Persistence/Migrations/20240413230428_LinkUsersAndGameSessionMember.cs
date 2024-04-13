using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LinkUsersAndGameSessionMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionMembers_UserId",
                table: "GameSessionMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameSessionId",
                table: "GameRounds",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_GameSessions_GameSessionId",
                table: "GameRounds",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionMembers_AspNetUsers_UserId",
                table: "GameSessionMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameSessions_GameSessionId",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionMembers_AspNetUsers_UserId",
                table: "GameSessionMembers");

            migrationBuilder.DropIndex(
                name: "IX_GameSessionMembers_UserId",
                table: "GameSessionMembers");

            migrationBuilder.DropIndex(
                name: "IX_GameRounds_GameSessionId",
                table: "GameRounds");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
