using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class Add_UserNameSender_IntoBroadCast : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "UserNameSender",
				table: "BroadCast",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 25, 8, 38, 37, 489, DateTimeKind.Local).AddTicks(4910));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "UserNameSender",
				table: "BroadCast");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 9, 16, 51, 21, 772, DateTimeKind.Local).AddTicks(7297));
		}
	}
}
