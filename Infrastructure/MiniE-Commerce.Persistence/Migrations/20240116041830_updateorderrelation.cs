using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniE_Commerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateorderrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 16, 7, 18, 30, 290, DateTimeKind.Local).AddTicks(4462));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 16, 5, 21, 49, 868, DateTimeKind.Local).AddTicks(7586));
        }
    }
}
