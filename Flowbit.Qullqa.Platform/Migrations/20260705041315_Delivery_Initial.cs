using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Flowbit.Qullqa.Platform.Migrations
{
    /// <inheritdoc />
    public partial class Delivery_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    business_id = table.Column<int>(type: "int", nullable: false),
                    tracking_number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    order_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    supplier_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    origin = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    destination = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    driver_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    driver_phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    vehicle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    license_plate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    registered_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    estimated_arrival = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    completed_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    current_label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    total_weight_value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_weight_unit = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    purchase_detail_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveries", x => x.id);
                    table.ForeignKey(
                        name: "fk_deliveries_business_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_deliveries_purchase_order_detail_purchase_detail_id",
                        column: x => x.purchase_detail_id,
                        principalTable: "purchase_order_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "delivery_current_locations",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<double>(type: "double", nullable: false),
                    longitude = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_current_locations", x => x.delivery_id);
                    table.ForeignKey(
                        name: "fk_delivery_current_locations_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "waypoints",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    delivery_id = table.Column<int>(type: "int", nullable: false),
                    label = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    reached = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sequence_order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_waypoints", x => x.id);
                    table.ForeignKey(
                        name: "fk_waypoints_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "waypoint_locations",
                columns: table => new
                {
                    waypoint_id = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<double>(type: "double", nullable: false),
                    longitude = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_waypoint_locations", x => x.waypoint_id);
                    table.ForeignKey(
                        name: "fk_waypoint_locations_waypoints_waypoint_id",
                        column: x => x.waypoint_id,
                        principalTable: "waypoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_business_id",
                table: "deliveries",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_purchase_detail_id",
                table: "deliveries",
                column: "purchase_detail_id");

            migrationBuilder.CreateIndex(
                name: "ix_waypoints_delivery_id",
                table: "waypoints",
                column: "delivery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery_current_locations");

            migrationBuilder.DropTable(
                name: "waypoint_locations");

            migrationBuilder.DropTable(
                name: "waypoints");

            migrationBuilder.DropTable(
                name: "deliveries");
        }
    }
}
