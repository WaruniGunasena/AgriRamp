using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriState_MiniTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3241), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3242) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3245), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3246) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3248), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3248) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3250), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3251) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3253), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3253) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3256), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3256) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3259), new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(3259) });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 6, 10, 43, 51, 580, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@agristate.com", "AQAAAAIAAYagAAAAEEtW5fN3s2+0ubSoqCAcF8CKO2/paR/DJ8R3yQ4OPk3MzpAyj04sMLfB7ITZRBIT4Q==", "Admin", "admin" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "user@agristate.com", "AQAAAAIAAYagAAAAEFg1F2AWICXwTRKPdQmco0ntRG2tOzjyY7J5Wo06thLg1UkYHM6BMrvliax+UP4GVQ==", "User", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4350), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4356), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4356) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4358), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4358) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4359), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4361), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4361) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4363), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4363) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4365), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4365) });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "ItemId",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4366), new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4366) });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4230));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4234));

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 5, 10, 57, 11, 144, DateTimeKind.Utc).AddTicks(4236));
        }
    }
}
