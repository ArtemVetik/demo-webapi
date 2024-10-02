using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "confirm_registration_codes",
                columns: table => new
                {
                    email = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    expire_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_confirm_registration_codes", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "player_credentials",
                columns: table => new
                {
                    player_id = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_credentials", x => x.player_id);
                });

            migrationBuilder.CreateTable(
                name: "player_profiles",
                columns: table => new
                {
                    player_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_profiles", x => x.player_id);
                    table.ForeignKey(
                        name: "FK_player_profiles_player_credentials_player_id",
                        column: x => x.player_id,
                        principalTable: "player_credentials",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "custom_maps",
                columns: table => new
                {
                    map_id = table.Column<string>(type: "text", nullable: false),
                    player_id = table.Column<string>(type: "text", nullable: false),
                    download_url = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custom_maps", x => x.map_id);
                    table.ForeignKey(
                        name: "FK_custom_maps_player_profiles_player_id",
                        column: x => x.player_id,
                        principalTable: "player_profiles",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    player_id = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.player_id);
                    table.ForeignKey(
                        name: "FK_subscriptions_player_profiles_player_id",
                        column: x => x.player_id,
                        principalTable: "player_profiles",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "downloads",
                columns: table => new
                {
                    player_id = table.Column<string>(type: "text", nullable: false),
                    map_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_downloads", x => new { x.player_id, x.map_id });
                    table.ForeignKey(
                        name: "FK_downloads_custom_maps_map_id",
                        column: x => x.map_id,
                        principalTable: "custom_maps",
                        principalColumn: "map_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_downloads_player_profiles_player_id",
                        column: x => x.player_id,
                        principalTable: "player_profiles",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_custom_maps_player_id",
                table: "custom_maps",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_downloads_map_id",
                table: "downloads",
                column: "map_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "confirm_registration_codes");

            migrationBuilder.DropTable(
                name: "downloads");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "custom_maps");

            migrationBuilder.DropTable(
                name: "player_profiles");

            migrationBuilder.DropTable(
                name: "player_credentials");
        }
    }
}
