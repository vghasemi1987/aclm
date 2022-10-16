using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class constra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2022, 10, 10, 8, 36, 9, 512, DateTimeKind.Local).AddTicks(7267));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationUserItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDateTime",
                value: new DateTime(2022, 9, 7, 14, 10, 36, 606, DateTimeKind.Local).AddTicks(6179));
        }
    }
}
