using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class init05 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_TransactionDetails_TransactionHeaders_HeaderId",
				table: "TransactionDetails");

			migrationBuilder.AlterColumn<DateTime>(
				name: "TransactionDate",
				table: "TransactionDetails",
				type: "datetime2",
				nullable: true,
				oldClrType: typeof(DateTime),
				oldType: "datetime2");

			migrationBuilder.AlterColumn<DateTime>(
				name: "LogDate",
				table: "TransactionDetails",
				type: "datetime2",
				nullable: true,
				oldClrType: typeof(DateTime),
				oldType: "datetime2");

			migrationBuilder.AlterColumn<int>(
				name: "HeaderId",
				table: "TransactionDetails",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<long>(
				name: "Amount",
				table: "TransactionDetails",
				type: "bigint",
				nullable: true,
				oldClrType: typeof(long),
				oldType: "bigint");

			migrationBuilder.AddColumn<string>(
				name: "QualificationsToken_Second",
				table: "ApplicationUserItems",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "Qualifications_Second",
				table: "ApplicationUserItems",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.CreateTable(
				name: "AbundanceReports",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SuccessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<int>(type: "int", nullable: false),
					UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Count = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AbundanceReports", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AccessibilityReportByCount",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Count = table.Column<int>(type: "int", nullable: false),
					DestinationIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DestinationSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessibilityReportByCount", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AccessibilityReportByFilter",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SourceIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SourceSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SourcePort = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DestinationIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DestinationSystemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DestinationPort = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RequestingUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConfirmerUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RequestDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
					AccessType = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessibilityReportByFilter", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "BroadCast",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					PersonnelCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					FileAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
					FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BroadCast", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ReferralBroadCast",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SrcUser = table.Column<int>(type: "int", nullable: false),
					DstUserID = table.Column<int>(type: "int", nullable: false),
					Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
					Status = table.Column<int>(type: "int", nullable: false),
					DeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
					IsImmediate = table.Column<bool>(type: "bit", nullable: false),
					BroadCastId = table.Column<int>(type: "int", nullable: false),
					RecordStatus = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ReferralBroadCast", x => x.Id);
					table.ForeignKey(
						name: "FK_ReferralBroadCast_BroadCast_BroadCastId",
						column: x => x.BroadCastId,
						principalTable: "BroadCast",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2021, 12, 18, 15, 25, 15, 309, DateTimeKind.Local).AddTicks(536));

			migrationBuilder.CreateIndex(
				name: "IX_ReferralBroadCast_BroadCastId",
				table: "ReferralBroadCast",
				column: "BroadCastId");

			migrationBuilder.AddForeignKey(
				name: "FK_TransactionDetails_TransactionHeaders_HeaderId",
				table: "TransactionDetails",
				column: "HeaderId",
				principalTable: "TransactionHeaders",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_TransactionDetails_TransactionHeaders_HeaderId",
				table: "TransactionDetails");

			migrationBuilder.DropTable(
				name: "AbundanceReports");

			migrationBuilder.DropTable(
				name: "AccessibilityReportByCount");

			migrationBuilder.DropTable(
				name: "AccessibilityReportByFilter");

			migrationBuilder.DropTable(
				name: "ReferralBroadCast");

			migrationBuilder.DropTable(
				name: "BroadCast");

			migrationBuilder.DropColumn(
				name: "QualificationsToken_Second",
				table: "ApplicationUserItems");

			migrationBuilder.DropColumn(
				name: "Qualifications_Second",
				table: "ApplicationUserItems");

			migrationBuilder.AlterColumn<DateTime>(
				name: "TransactionDate",
				table: "TransactionDetails",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
				oldClrType: typeof(DateTime),
				oldType: "datetime2",
				oldNullable: true);

			migrationBuilder.AlterColumn<DateTime>(
				name: "LogDate",
				table: "TransactionDetails",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
				oldClrType: typeof(DateTime),
				oldType: "datetime2",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "HeaderId",
				table: "TransactionDetails",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<long>(
				name: "Amount",
				table: "TransactionDetails",
				type: "bigint",
				nullable: false,
				defaultValue: 0L,
				oldClrType: typeof(long),
				oldType: "bigint",
				oldNullable: true);

			migrationBuilder.UpdateData(
				table: "ApplicationUserItems",
				keyColumn: "Id",
				keyValue: 1,
				column: "RegDateTime",
				value: new DateTime(2021, 6, 21, 16, 48, 0, 309, DateTimeKind.Local).AddTicks(898));

			migrationBuilder.AddForeignKey(
				name: "FK_TransactionDetails_TransactionHeaders_HeaderId",
				table: "TransactionDetails",
				column: "HeaderId",
				principalTable: "TransactionHeaders",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
