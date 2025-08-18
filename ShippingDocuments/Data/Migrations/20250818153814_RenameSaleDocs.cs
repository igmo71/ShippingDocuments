using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingDocuments.Migrations
{
    /// <inheritdoc />
    public partial class RenameSaleDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaperworkErrors_SaleDoc_SaleDocId",
                table: "PaperworkErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_QuantityErrors_SaleDoc_SaleDocId",
                table: "QuantityErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDoc_AspNetUsers_UserId",
                table: "SaleDoc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDoc",
                table: "SaleDoc");

            migrationBuilder.RenameTable(
                name: "SaleDoc",
                newName: "SaleDocs");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDoc_UserId",
                table: "SaleDocs",
                newName: "IX_SaleDocs_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SaleDocs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDocs",
                table: "SaleDocs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaperworkErrors_SaleDocs_SaleDocId",
                table: "PaperworkErrors",
                column: "SaleDocId",
                principalTable: "SaleDocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuantityErrors_SaleDocs_SaleDocId",
                table: "QuantityErrors",
                column: "SaleDocId",
                principalTable: "SaleDocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDocs_AspNetUsers_UserId",
                table: "SaleDocs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaperworkErrors_SaleDocs_SaleDocId",
                table: "PaperworkErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_QuantityErrors_SaleDocs_SaleDocId",
                table: "QuantityErrors");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDocs_AspNetUsers_UserId",
                table: "SaleDocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDocs",
                table: "SaleDocs");

            migrationBuilder.RenameTable(
                name: "SaleDocs",
                newName: "SaleDoc");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDocs_UserId",
                table: "SaleDoc",
                newName: "IX_SaleDoc_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SaleDoc",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDoc",
                table: "SaleDoc",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaperworkErrors_SaleDoc_SaleDocId",
                table: "PaperworkErrors",
                column: "SaleDocId",
                principalTable: "SaleDoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuantityErrors_SaleDoc_SaleDocId",
                table: "QuantityErrors",
                column: "SaleDocId",
                principalTable: "SaleDoc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDoc_AspNetUsers_UserId",
                table: "SaleDoc",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
