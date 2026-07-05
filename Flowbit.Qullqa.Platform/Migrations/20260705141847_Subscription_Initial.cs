using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flowbit.Qullqa.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Subscription_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    time_length = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    features = table.Column<string>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plans", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "plans",
                columns: new[] { "id", "currency", "description", "features", "name", "price", "status", "time_length" },
                values: new object[,]
                {
                    { 1, "PEN", "Ideal para pequeñas bodegas y farmacias", "[\"Inventario básico\",\"Ventas POS\",\"1 almacén\",\"Hasta 100 productos\"]", "Plan Básico", 19.9m, "ACTIVE", "MONTHLY" },
                    { 2, "PEN", "Para negocios en crecimiento", "[\"Todo el Plan Básico\",\"Alertas inteligentes\",\"3 almacenes\",\"Proveedores ilimitados\",\"Reportes avanzados\",\"Tracking IoT\"]", "Plan Pro", 49.9m, "ACTIVE", "MONTHLY" },
                    { 3, "PEN", "Para cadenas de tiendas y farmacias", "[\"Todo el Plan Pro\",\"Almacenes ilimitados\",\"Soporte prioritario\",\"API dedicada\",\"Multi-negocio\"]", "Plan Enterprise", 99.9m, "ACTIVE", "MONTHLY" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_businesses_plan_id",
                table: "businesses",
                column: "plan_id");

            migrationBuilder.AddForeignKey(
                name: "fk_businesses_plan_plan_id",
                table: "businesses",
                column: "plan_id",
                principalTable: "plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_businesses_plan_plan_id",
                table: "businesses");

            migrationBuilder.DropTable(
                name: "plans");

            migrationBuilder.DropIndex(
                name: "ix_businesses_plan_id",
                table: "businesses");
        }
    }
}
