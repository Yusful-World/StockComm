using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockComm.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eaac66e-09b2-41c5-8145-e1dad7018828");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a7938a7-ea48-4fcb-a161-a1c71e09a20f");

            migrationBuilder.CreateTable(
                name: "UserPortfolios",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPortfolios", x => new { x.AppUserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_UserPortfolios_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPortfolios_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38461ce5-8f3a-4767-b83e-85191a484871", null, "User", "USER" },
                    { "dfaf9d20-5a32-4ce5-9be7-b4fe888f7c42", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPortfolios_StockId",
                table: "UserPortfolios",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPortfolios");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38461ce5-8f3a-4767-b83e-85191a484871");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfaf9d20-5a32-4ce5-9be7-b4fe888f7c42");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3eaac66e-09b2-41c5-8145-e1dad7018828", null, "Admin", "ADMIN" },
                    { "4a7938a7-ea48-4fcb-a161-a1c71e09a20f", null, "User", "USER" }
                });
        }
    }
}
