using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machines_productionstages_ProductionStageId",
                table: "machines");

            migrationBuilder.AlterColumn<int>(
                name: "ProductionStageId",
                table: "machines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_machines_productionstages_ProductionStageId",
                table: "machines",
                column: "ProductionStageId",
                principalTable: "productionstages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machines_productionstages_ProductionStageId",
                table: "machines");

            migrationBuilder.AlterColumn<int>(
                name: "ProductionStageId",
                table: "machines",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_machines_productionstages_ProductionStageId",
                table: "machines",
                column: "ProductionStageId",
                principalTable: "productionstages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
