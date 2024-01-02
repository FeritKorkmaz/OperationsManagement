using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineOrderId",
                table: "machines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "machineorders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    OrderQuantity = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machineorders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_machines_MachineOrderId",
                table: "machines",
                column: "MachineOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_machines_machineorders_MachineOrderId",
                table: "machines",
                column: "MachineOrderId",
                principalTable: "machineorders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machines_machineorders_MachineOrderId",
                table: "machines");

            migrationBuilder.DropTable(
                name: "machineorders");

            migrationBuilder.DropIndex(
                name: "IX_machines_MachineOrderId",
                table: "machines");

            migrationBuilder.DropColumn(
                name: "MachineOrderId",
                table: "machines");
        }
    }
}
