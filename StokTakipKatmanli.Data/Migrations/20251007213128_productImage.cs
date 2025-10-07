using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokTakipKatmanli.Data.Migrations
{
    /// <inheritdoc />
    public partial class productImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 10, 8, 0, 31, 28, 57, DateTimeKind.Local).AddTicks(9556));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 10, 6, 23, 34, 48, 745, DateTimeKind.Local).AddTicks(4268));
        }
    }
}
