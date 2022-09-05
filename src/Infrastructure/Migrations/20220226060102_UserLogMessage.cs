using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class UserLogMessage : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "UserLogMessage",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					Controller = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					DateActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserLogMessage", x => x.Id);
				});

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 26, 9, 31, 0, 259, DateTimeKind.Local).AddTicks(8428));
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserLogMessage");

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2022, 2, 25, 8, 38, 37, 489, DateTimeKind.Local).AddTicks(4910));
		}
	}
}
