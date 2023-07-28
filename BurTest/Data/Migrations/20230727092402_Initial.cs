using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurTest.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_company",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_telemetry",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    depth = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_telemetry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t_well",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    active = table.Column<int>(type: "INTEGER", nullable: false),
                    id_telemetry = table.Column<int>(type: "INTEGER", nullable: false),
                    id_company = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_well", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_well_t_company_id_company",
                        column: x => x.id_company,
                        principalTable: "t_company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_well_t_telemetry_id_telemetry",
                        column: x => x.id_telemetry,
                        principalTable: "t_telemetry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_telemetry_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    depth = table.Column<float>(type: "REAL", nullable: false),
                    id_well = table.Column<int>(type: "INTEGER", nullable: false),
                    id_telemetry = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_telemetry_history", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_telemetry_history_t_telemetry_id_telemetry",
                        column: x => x.id_telemetry,
                        principalTable: "t_telemetry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_telemetry_history_t_well_id_well",
                        column: x => x.id_well,
                        principalTable: "t_well",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_telemetry_history_id_telemetry",
                table: "t_telemetry_history",
                column: "id_telemetry");

            migrationBuilder.CreateIndex(
                name: "IX_t_telemetry_history_id_well",
                table: "t_telemetry_history",
                column: "id_well");

            migrationBuilder.CreateIndex(
                name: "IX_t_well_id_company",
                table: "t_well",
                column: "id_company");

            migrationBuilder.CreateIndex(
                name: "IX_t_well_id_telemetry",
                table: "t_well",
                column: "id_telemetry",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_telemetry_history");

            migrationBuilder.DropTable(
                name: "t_well");

            migrationBuilder.DropTable(
                name: "t_company");

            migrationBuilder.DropTable(
                name: "t_telemetry");
        }
    }
}
