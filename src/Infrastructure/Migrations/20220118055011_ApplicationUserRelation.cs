using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class ApplicationUserRelation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "ApplicationUserID",
				table: "ReferralBroadCast",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 18, 9, 20, 10, 207, DateTimeKind.Local).AddTicks(8538));

			migrationBuilder.CreateIndex(
				name: "IX_ReferralBroadCast_ApplicationUserID",
				table: "ReferralBroadCast",
				column: "ApplicationUserID");

			migrationBuilder.AddForeignKey(
				name: "FK_ReferralBroadCast_ApplicationUserItems_ApplicationUserID",
				table: "ReferralBroadCast",
				column: "ApplicationUserID",
				principalTable: "ApplicationUserItems",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ReferralBroadCast_ApplicationUserItems_ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.DropIndex(
				name: "IX_ReferralBroadCast_ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.DropColumn(
				name: "ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 16, 15, 28, 26, 396, DateTimeKind.Local).AddTicks(4717));
		}
	}
}
