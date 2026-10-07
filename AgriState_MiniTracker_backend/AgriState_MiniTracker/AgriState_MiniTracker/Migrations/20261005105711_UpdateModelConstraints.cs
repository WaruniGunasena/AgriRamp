using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriState_MiniTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "LocationId", "Name" },
                values: new object[,]
                {
                    { 101, "Nutrients and fertilizers", 1, "Fertilizers" },
                    { 102, "Farm and estate tools", 1, "Tools" },
                    { 103, "Crop seeds and seedlings", 2, "Seeds" },
                    { 104, "Eco pesticides and fungicides", 2, "Pesticides" }
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                columns: new[] { "Code", "CreatedAt", "Description", "Name" },
                values: new object[] { "LOC-RAT", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4230), "Main Barn Storage", "Rathnapura" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                columns: new[] { "Code", "CreatedAt", "Description", "Name" },
                values: new object[] { "LOC-KDY", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4234), "Highland Greenhouse", "Kandy" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Code", "CreatedAt", "Description", "Name" },
                values: new object[] { 3, "LOC-KEG", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4236), "Processing Facility", "Kegalle" });

            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "ItemId", "CategoryId", "CreatedAt", "LocationId", "MinStockLevel", "Name", "Quantity", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 101, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4350), 1, 10m, "Fertilizer", 45m, "Bags", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4350) },
                    { 2, 101, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4356), 1, 15m, "Organic Compost", 8m, "Tons", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4356) },
                    { 3, 102, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4358), 1, 5m, "Pruning Shears", 24m, "Units", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4358) },
                    { 4, 102, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4359), 1, 5m, "Tractor Oil", 3m, "Pcs", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4360) },
                    { 5, 103, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4361), 2, 20m, "Tomato Seeds", 120m, "Packets", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4361) },
                    { 6, 103, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4363), 2, 15m, "Nutrient Solution", 60m, "Liters", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4363) },
                    { 7, 104, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4365), 2, 10m, "Bio-Neem Eco Pesticide", 5m, "Bottles", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4365) },
                    { 8, 104, new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4366), 2, 8m, "Fungicide Spray Max", 18m, "Bottles", new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4366) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 104);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "Fertilizers & Soil" },
                    { 2, null, 1, "Heavy Equipment" },
                    { 3, null, 2, "Seeds & Seedlings" }
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                columns: new[] { "Code", "CreatedAt", "Description", "Name" },
                values: new object[] { "LOC-NB", new DateTime(2026, 10, 5, 9, 44, 4, 504, DateTimeKind.Utc).AddTicks(8932), "Main storage", "North Barn" });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                columns: new[] { "Code", "CreatedAt", "Description", "Name" },
                values: new object[] { "LOC-SG", new DateTime(2026, 10, 5, 9, 44, 4, 504, DateTimeKind.Utc).AddTicks(8935), "Nursery", "South Greenhouse" });
        }
    }
}
