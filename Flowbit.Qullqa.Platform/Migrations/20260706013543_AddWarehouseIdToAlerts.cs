using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowbit.Qullqa.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseIdToAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "warehouse_id",
                table: "alerts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_alerts_warehouse_id",
                table: "alerts",
                column: "warehouse_id");

            migrationBuilder.AddForeignKey(
                name: "fk_alerts_warehouse_warehouse_id",
                table: "alerts",
                column: "warehouse_id",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_alerts_warehouse_warehouse_id",
                table: "alerts");

            migrationBuilder.DropIndex(
                name: "ix_alerts_warehouse_id",
                table: "alerts");

            migrationBuilder.DropColumn(
                name: "warehouse_id",
                table: "alerts");
        }
    }
}
