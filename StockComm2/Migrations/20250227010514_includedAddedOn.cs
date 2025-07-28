using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockComm.Migrations
{
    /// <inheritdoc />
    public partial class includedAddedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04ee5891-8f36-4b25-9945-04c6502b87b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9386855-0639-4823-85b0-a3b4a41d1a29");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0518ed23-fe8e-4655-ba8d-d99f5b1d31e0", null, "User", "USER" },
                    { "aac6512c-f222-44db-a5c6-2d379480c00a", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0518ed23-fe8e-4655-ba8d-d99f5b1d31e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aac6512c-f222-44db-a5c6-2d379480c00a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04ee5891-8f36-4b25-9945-04c6502b87b8", null, "User", "USER" },
                    { "d9386855-0639-4823-85b0-a3b4a41d1a29", null, "Admin", "ADMIN" }
                });
        }
    }
}
