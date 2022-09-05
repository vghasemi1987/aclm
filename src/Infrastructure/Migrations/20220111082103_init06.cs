using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class init06 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ActionDescription",
				table: "ReferralBroadCast",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 11, 11, 51, 0, 208, DateTimeKind.Local).AddTicks(1726));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ActionDescription",
				table: "ReferralBroadCast");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2021, 12, 28, 15, 46, 30, 107, DateTimeKind.Local).AddTicks(971));
		}
	}
}
