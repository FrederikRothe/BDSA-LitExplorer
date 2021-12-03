using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LitExplore.ApplicationLogic.Migrations
{
    public partial class DBAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionTeam",
                columns: table => new
                {
                    ConnectionsId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionTeam", x => new { x.ConnectionsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_ConnectionTeam_Connections_ConnectionsId",
                        column: x => x.ConnectionsId,
                        principalTable: "Connections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionTeam_TeamsId",
                table: "ConnectionTeam",
                column: "TeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionTeam");
        }
    }
}
