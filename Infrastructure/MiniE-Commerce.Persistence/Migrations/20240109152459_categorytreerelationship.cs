using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniE_Commerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categorytreerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priorty",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "ParrentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "ParrentCategoryId", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 1, 9, 18, 24, 58, 628, DateTimeKind.Local).AddTicks(7160), false, "BaseCategory", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ParrentCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Priorty",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
