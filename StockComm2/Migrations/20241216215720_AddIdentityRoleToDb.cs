using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockComm.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityRoleToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3eaac66e-09b2-41c5-8145-e1dad7018828", null, "Admin", "ADMIN" },
                    { "4a7938a7-ea48-4fcb-a161-a1c71e09a20f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eaac66e-09b2-41c5-8145-e1dad7018828");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a7938a7-ea48-4fcb-a161-a1c71e09a20f");
        }
    }
}
