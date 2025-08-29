using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingDocuments.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSaleDocLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "SaleDocLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SaleDocLogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDocLogs_UserId",
                table: "SaleDocLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDocLogs_AspNetUsers_UserId",
                table: "SaleDocLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDocLogs_AspNetUsers_UserId",
                table: "SaleDocLogs");

            migrationBuilder.DropIndex(
                name: "IX_SaleDocLogs_UserId",
                table: "SaleDocLogs");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "SaleDocLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SaleDocLogs");
        }
    }
}
