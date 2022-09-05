using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class ApplicationUserRelation02 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ReferralBroadCast_ApplicationUserItems_ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.DropForeignKey(
				name: "FK_ReferralBroadCast_BroadCast_BroadCastId",
				table: "ReferralBroadCast");

			migrationBuilder.DropIndex(
				name: "IX_ReferralBroadCast_ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.DropColumn(
				name: "ApplicationUserID",
				table: "ReferralBroadCast");

			migrationBuilder.AlterColumn<int>(
				name: "SrcUser",
				table: "ReferralBroadCast",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "DstUserID",
				table: "ReferralBroadCast",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "BroadCastId",
				table: "ReferralBroadCast",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 18, 12, 25, 12, 798, DateTimeKind.Local).AddTicks(1514));

			migrationBuilder.CreateIndex(
				name: "IX_ReferralBroadCast_DstUserID",
				table: "ReferralBroadCast",
				column: "DstUserID");

			migrationBuilder.AddForeignKey(
				name: "FK_ReferralBroadCast_ApplicationUserItems_DstUserID",
				table: "ReferralBroadCast",
				column: "DstUserID",
				principalTable: "ApplicationUserItems",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_ReferralBroadCast_BroadCast_BroadCastId",
				table: "ReferralBroadCast",
				column: "BroadCastId",
				principalTable: "BroadCast",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ReferralBroadCast_ApplicationUserItems_DstUserID",
				table: "ReferralBroadCast");

			migrationBuilder.DropForeignKey(
				name: "FK_ReferralBroadCast_BroadCast_BroadCastId",
				table: "ReferralBroadCast");

			migrationBuilder.DropIndex(
				name: "IX_ReferralBroadCast_DstUserID",
				table: "ReferralBroadCast");

			migrationBuilder.AlterColumn<int>(
				name: "SrcUser",
				table: "ReferralBroadCast",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "DstUserID",
				table: "ReferralBroadCast",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "BroadCastId",
				table: "ReferralBroadCast",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

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

			migrationBuilder.AddForeignKey(
				name: "FK_ReferralBroadCast_BroadCast_BroadCastId",
				table: "ReferralBroadCast",
				column: "BroadCastId",
				principalTable: "BroadCast",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
