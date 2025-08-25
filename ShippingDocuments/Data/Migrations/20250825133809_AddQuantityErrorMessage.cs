using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingDocuments.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityErrorMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "QuantityErrors",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "QuantityErrors");
        }
    }
}
