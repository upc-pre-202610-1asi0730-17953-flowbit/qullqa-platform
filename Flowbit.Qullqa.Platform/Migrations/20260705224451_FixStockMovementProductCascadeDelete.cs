using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowbit.Qullqa.Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixStockMovementProductCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_movements_products_product_id",
                table: "stock_movements");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_movements_products_product_id",
                table: "stock_movements",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stock_movements_products_product_id",
                table: "stock_movements");

            migrationBuilder.AddForeignKey(
                name: "fk_stock_movements_products_product_id",
                table: "stock_movements",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
