using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniE_Commerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addorderdetailsqaunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 16, 7, 31, 59, 999, DateTimeKind.Local).AddTicks(720));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 16, 7, 24, 33, 205, DateTimeKind.Local).AddTicks(5805));
        }
    }
}
