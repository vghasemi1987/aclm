using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class ProtectionOfficeddd : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "MessageUserId",
				table: "ApplicationUserItems",
				type: "int",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "MessageUser",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MessageUser", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ProtectionOffices",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					MessageUserId = table.Column<int>(type: "int", nullable: true),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProtectionOffices", x => x.Id);
					table.ForeignKey(
						name: "FK_ProtectionOffices_MessageUser_MessageUserId",
						column: x => x.MessageUserId,
						principalTable: "MessageUser",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 16, 11, 9, 37, 431, DateTimeKind.Local).AddTicks(5591));

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_MessageUserId",
				table: "ApplicationUserItems",
				column: "MessageUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOffices_MessageUserId",
				table: "ProtectionOffices",
				column: "MessageUserId");

			migrationBuilder.AddForeignKey(
				name: "FK_ApplicationUserItems_MessageUser_MessageUserId",
				table: "ApplicationUserItems",
				column: "MessageUserId",
				principalTable: "MessageUser",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ApplicationUserItems_MessageUser_MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.DropTable(
				name: "ProtectionOffices");

			migrationBuilder.DropTable(
				name: "MessageUser");

			migrationBuilder.DropIndex(
				name: "IX_ApplicationUserItems_MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.DropColumn(
				name: "MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 11, 11, 51, 0, 208, DateTimeKind.Local).AddTicks(1726));
		}
	}
}
