using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class Add_ProtectionOfficeMember : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ProtectionOfficeMember",
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
					table.PrimaryKey("PK_ProtectionOfficeMember", x => x.Id);
					table.ForeignKey(
						name: "FK_ProtectionOfficeMember_ApplicationUserItems_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ProtectionOfficeMember_ProtectionOffices_ProtectionOfficeId",
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
				value: new DateTime(2022, 2, 8, 10, 27, 11, 882, DateTimeKind.Local).AddTicks(4978));

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOfficeMember_ApplicationUserId",
				table: "ProtectionOfficeMember",
				column: "ApplicationUserId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_ProtectionOfficeMember_ProtectionOfficeId",
				table: "ProtectionOfficeMember",
				column: "ProtectionOfficeId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ProtectionOfficeMember");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 1, 18, 12, 25, 12, 798, DateTimeKind.Local).AddTicks(1514));
		}
	}
}
