using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameSessionMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSessionMembers",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    GameSessionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Team = table.Column<byte>(type: "INTEGER", nullable: false),
                    TeamRole = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessionMembers", x => new { x.UserID, x.GameSessionID });
                    table.ForeignKey(
                        name: "FK_GameSessionMembers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSessionMembers_GameSessions_GameSessionID",
                        column: x => x.GameSessionID,
                        principalTable: "GameSessions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionMembers_GameSessionID",
                table: "GameSessionMembers",
                column: "GameSessionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessionMembers");
        }
    }
}
