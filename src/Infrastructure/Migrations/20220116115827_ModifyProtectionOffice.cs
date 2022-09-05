using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class ModifyProtectionOffice : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_ApplicationUserItems_MessageUser_MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.DropForeignKey(
				name: "FK_ProtectionOffices_MessageUser_MessageUserId",
				table: "ProtectionOffices");

			migrationBuilder.DropTable(
				name: "MessageUser");

			migrationBuilder.DropIndex(
				name: "IX_ProtectionOffices_MessageUserId",
				table: "ProtectionOffices");

			migrationBuilder.DropIndex(
				name: "IX_ApplicationUserItems_MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.DropColumn(
				name: "MessageUserId",
				table: "ProtectionOffices");

			migrationBuilder.DropColumn(
				name: "MessageUserId",
				table: "ApplicationUserItems");

			migrationBuilder.CreateTable(
				name: "ProtectionOfficeUserRelation",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ProtectionOfficeId = table.Column<int>(type: "int", nullable: false),
					ApplicationUserId = table.Column<int>(type: "int", nullable: false),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProtectionOfficeUserRelation", x => x.Id);
					table.ForeignKey(
						name: "FK_ProtectionOfficeUserRelation_ApplicationUserItems_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ProtectionOfficeUserRelation_ProtectionOffices_ProtectionOfficeId",
						column: x => x.ProtectionOfficeId,
						principalTable: "ProtectionOffices",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 16, 15, 28, 26, 396, DateTimeKind.Local).AddTicks(4717));

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOfficeUserRelation_ApplicationUserId",
				table: "ProtectionOfficeUserRelation",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOfficeUserRelation_ProtectionOfficeId",
				table: "ProtectionOfficeUserRelation",
				column: "ProtectionOfficeId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ProtectionOfficeUserRelation");

			migrationBuilder.AddColumn<int>(
				name: "MessageUserId",
				table: "ProtectionOffices",
				type: "int",
				nullable: true);

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

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 16, 13, 12, 47, 464, DateTimeKind.Local).AddTicks(9338));

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOffices_MessageUserId",
				table: "ProtectionOffices",
				column: "MessageUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_MessageUserId",
				table: "ApplicationUserItems",
				column: "MessageUserId");

			migrationBuilder.AddForeignKey(
				name: "FK_ApplicationUserItems_MessageUser_MessageUserId",
				table: "ApplicationUserItems",
				column: "MessageUserId",
				principalTable: "MessageUser",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_ProtectionOffices_MessageUser_MessageUserId",
				table: "ProtectionOffices",
				column: "MessageUserId",
				principalTable: "MessageUser",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
