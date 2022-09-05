using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class Add_GroupingOffice : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "GroupingOffice",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GroupingOffice", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "GroupingOfficeMember",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					GroupingOfficeId = table.Column<int>(type: "int", nullable: false),
					ApplicationUserId = table.Column<int>(type: "int", nullable: false),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_GroupingOfficeMember", x => x.Id);
					table.ForeignKey(
						name: "FK_GroupingOfficeMember_ApplicationUserItems_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_GroupingOfficeMember_GroupingOffice_GroupingOfficeId",
						column: x => x.GroupingOfficeId,
						principalTable: "GroupingOffice",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 9, 16, 51, 21, 772, DateTimeKind.Local).AddTicks(7297));

			migrationBuilder.CreateIndex(
				name: "IX_GroupingOfficeMember_ApplicationUserId",
				table: "GroupingOfficeMember",
				column: "ApplicationUserId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_GroupingOfficeMember_GroupingOfficeId",
				table: "GroupingOfficeMember",
				column: "GroupingOfficeId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "GroupingOfficeMember");

			migrationBuilder.DropTable(
				name: "GroupingOffice");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 8, 10, 27, 11, 882, DateTimeKind.Local).AddTicks(4978));
		}
	}
}
