using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
	public partial class init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AccessibilityLevels",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessibilityLevels", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AccessRangeHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessRangeHeaders", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AccessTypes",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false),
					Description = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ActionTypes",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ActionTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AddressTypes",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false),
					Description = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AddressTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_OrganizationalCharts",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_OrganizationalCharts", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_Roles",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PanelMenu = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_Roles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Authorities",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false),
					Description = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Authorities", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "BankBranches",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					BranchHeadId = table.Column<int>(nullable: true),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BankBranches", x => x.Id);
					table.ForeignKey(
						name: "FK_BankBranches_BankBranches_BranchHeadId",
						column: x => x.BranchHeadId,
						principalTable: "BankBranches",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Common_Priorities",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 20, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Common_Priorities", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Common_TestOptions",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					TableId = table.Column<short>(nullable: false),
					Title = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Common_TestOptions", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Departments", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DutyPositions",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Code = table.Column<int>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DutyPositions", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ExcelFormatHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					ExcelHeaders = table.Column<string>(maxLength: 500, nullable: true),
					FileFormat = table.Column<byte>(nullable: false),
					Separator = table.Column<string>(maxLength: 5, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ExcelFormatHeaders", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Notification_Categories",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Notification_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Positions",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Positions", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Protocols",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 200, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Protocols", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Report_Boxes",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 100, nullable: true),
					Icon = table.Column<string>(maxLength: 50, nullable: true),
					Key = table.Column<string>(maxLength: 50, nullable: true),
					Link = table.Column<string>(maxLength: 150, nullable: true),
					AccessRight = table.Column<string>(maxLength: 50, nullable: true),
					BoxStatus = table.Column<string>(maxLength: 100, nullable: true),
					SqlCommand = table.Column<string>(maxLength: 500, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Report_Boxes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Report_Charts",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 100, nullable: true),
					Type = table.Column<string>(nullable: true),
					Command = table.Column<string>(nullable: true),
					SeriesName = table.Column<string>(nullable: true),
					ClassName = table.Column<string>(nullable: true),
					Style = table.Column<string>(nullable: true),
					Color = table.Column<string>(nullable: true),
					ChartOptions = table.Column<string>(nullable: true),
					AccessRight = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Report_Charts", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Routers",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: true),
					AccessNo = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Routers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ServiceGroups",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 256, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ServiceGroups", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ServiceLevels",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ServiceLevels", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Settings",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					EmailUsername = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
					EmailPassword = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
					EmailSmtpServer = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
					EmailPortNumber = table.Column<short>(nullable: true),
					EnableSsl = table.Column<bool>(nullable: true),
					WelcomeText = table.Column<string>(nullable: true),
					WebSiteTitle = table.Column<string>(maxLength: 100, nullable: true),
					SmsServiceNumber = table.Column<string>(maxLength: 50, nullable: true),
					SmsUserName = table.Column<string>(nullable: true),
					SmsPassword = table.Column<string>(maxLength: 100, nullable: true),
					ThanksMsg = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Settings", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ToDoTask_States",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ToDoTask_States", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ToDoTask_UsageTypes",
				columns: table => new
				{
					Id = table.Column<short>(nullable: false),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ToDoTask_UsageTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TopupAccountExcelFormatHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					ExcelHeaders = table.Column<string>(maxLength: 500, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupAccountExcelFormatHeaders", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "TopupChannelExcelFormatHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					ExcelHeaders = table.Column<string>(maxLength: 500, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupChannelExcelFormatHeaders", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AccessRangeDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					IpFrom = table.Column<string>(maxLength: 50, nullable: false),
					IpTo = table.Column<string>(maxLength: 50, nullable: false),
					AccessRangeHeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessRangeDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_AccessRangeDetails_AccessRangeHeaders_AccessRangeHeaderId",
						column: x => x.AccessRangeHeaderId,
						principalTable: "AccessRangeHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_RoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RoleId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_RoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationUser_RoleClaims_ApplicationUser_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "ApplicationUser_Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationRoleAuthorities",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					ApplicationRoleId = table.Column<int>(nullable: false),
					AuthorityId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationRoleAuthorities", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationRoleAuthorities_ApplicationUser_Roles_ApplicationRoleId",
						column: x => x.ApplicationRoleId,
						principalTable: "ApplicationUser_Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ApplicationRoleAuthorities_Authorities_AuthorityId",
						column: x => x.AuthorityId,
						principalTable: "Authorities",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Systems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					SystemName = table.Column<string>(maxLength: 50, nullable: false),
					IpAddress = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 250, nullable: true),
					AccessibilityLevelId = table.Column<int>(nullable: true),
					InformationAccessibilityLevelId = table.Column<int>(nullable: true),
					KindId = table.Column<int>(nullable: false),
					ImportanceFactor = table.Column<int>(nullable: true),
					PersonelCode = table.Column<int>(nullable: true),
					FirstName = table.Column<string>(maxLength: 50, nullable: true),
					LastName = table.Column<string>(maxLength: 50, nullable: true),
					DepartmentId = table.Column<int>(nullable: true),
					IpFrom = table.Column<string>(nullable: true),
					IpTo = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Systems", x => x.Id);
					table.ForeignKey(
						name: "FK_Systems_AccessibilityLevels_AccessibilityLevelId",
						column: x => x.AccessibilityLevelId,
						principalTable: "AccessibilityLevels",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Systems_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Systems_AccessibilityLevels_InformationAccessibilityLevelId",
						column: x => x.InformationAccessibilityLevelId,
						principalTable: "AccessibilityLevels",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ExcelFormatDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Column = table.Column<string>(maxLength: 50, nullable: true),
					MappedColumn = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ExcelFormatDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_ExcelFormatDetails_ExcelFormatHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "ExcelFormatHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUserItems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserName = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					FirstName = table.Column<string>(maxLength: 150, nullable: false),
					LastName = table.Column<string>(maxLength: 150, nullable: false),
					PersonnelCode = table.Column<string>(maxLength: 10, nullable: true),
					Picture = table.Column<string>(maxLength: 150, nullable: true),
					RegDateTime = table.Column<DateTime>(nullable: false),
					DepartmentId = table.Column<int>(nullable: true),
					PositionId = table.Column<int>(nullable: true),
					BankBranchId = table.Column<int>(nullable: true),
					DutyPositionId = table.Column<int>(nullable: true),
					ExpertPerformance = table.Column<bool>(nullable: false),
					ExpertPerformanceToken = table.Column<string>(maxLength: 100, nullable: true),
					Qualifications = table.Column<bool>(nullable: false),
					QualificationsToken = table.Column<string>(maxLength: 100, nullable: true),

					//Qualifications = table.Column<bool>(nullable: false),
					//QualificationsToken = table.Column<string>(maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUserItems", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationUserItems_BankBranches_BankBranchId",
						column: x => x.BankBranchId,
						principalTable: "BankBranches",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ApplicationUserItems_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ApplicationUserItems_DutyPositions_DutyPositionId",
						column: x => x.DutyPositionId,
						principalTable: "DutyPositions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ApplicationUserItems_Positions_PositionId",
						column: x => x.PositionId,
						principalTable: "Positions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Policies",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 200, nullable: true),
					ProtocolId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Policies", x => x.Id);
					table.ForeignKey(
						name: "FK_Policies_Protocols_ProtocolId",
						column: x => x.ProtocolId,
						principalTable: "Protocols",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AclFilesUpload",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					FileName = table.Column<string>(maxLength: 50, nullable: false),
					Description = table.Column<string>(maxLength: 200, nullable: true),
					RouterId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AclFilesUpload", x => x.Id);
					table.ForeignKey(
						name: "FK_AclFilesUpload_Routers_RouterId",
						column: x => x.RouterId,
						principalTable: "Routers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Services",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					Description = table.Column<string>(maxLength: 200, nullable: false),
					Port = table.Column<int>(nullable: true),
					ProtocolId = table.Column<int>(nullable: true),
					ServiceLevelId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Services", x => x.Id);
					table.ForeignKey(
						name: "FK_Services_Protocols_ProtocolId",
						column: x => x.ProtocolId,
						principalTable: "Protocols",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Services_ServiceLevels_ServiceLevelId",
						column: x => x.ServiceLevelId,
						principalTable: "ServiceLevels",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TopupAccountExcelFormatDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Column = table.Column<string>(maxLength: 50, nullable: true),
					MappedColumn = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupAccountExcelFormatDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupAccountExcelFormatDetails_TopupAccountExcelFormatHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "TopupAccountExcelFormatHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TopupChannelExcelFormatDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Column = table.Column<string>(maxLength: 50, nullable: true),
					MappedColumn = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupChannelExcelFormatDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupChannelExcelFormatDetails_TopupChannelExcelFormatHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "TopupChannelExcelFormatHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_ActivityLogs",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					ActivityTitle = table.Column<string>(maxLength: 200, nullable: true),
					ActionName = table.Column<string>(maxLength: 150, nullable: true),
					ControllerName = table.Column<string>(maxLength: 150, nullable: true),
					UserId = table.Column<int>(nullable: false),
					EntryDateTime = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_ActivityLogs", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationUser_ActivityLogs_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_Claims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_Claims", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationUser_Claims_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_Logins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_Logins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_ApplicationUser_Logins_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_Tokens",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_Tokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_ApplicationUser_Tokens_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUser_UserRoles",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					RoleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUser_UserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_ApplicationUser_UserRoles_ApplicationUser_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "ApplicationUser_Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ApplicationUser_UserRoles_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ApplicationUserAccessRageHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					ApplicationUserId = table.Column<int>(nullable: false),
					AccessRangeHeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ApplicationUserAccessRageHeaders", x => x.Id);
					table.ForeignKey(
						name: "FK_ApplicationUserAccessRageHeaders_AccessRangeHeaders_AccessRangeHeaderId",
						column: x => x.AccessRangeHeaderId,
						principalTable: "AccessRangeHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ApplicationUserAccessRageHeaders_ApplicationUserItems_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SearchLogDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					FirstName = table.Column<string>(maxLength: 50, nullable: true),
					LastName = table.Column<string>(maxLength: 50, nullable: true),
					NationalCode = table.Column<string>(maxLength: 10, nullable: true),
					FatherName = table.Column<string>(maxLength: 50, nullable: true),
					Address = table.Column<string>(maxLength: 250, nullable: true),
					LetterIdentifier = table.Column<string>(nullable: true),
					CardNumber = table.Column<string>(maxLength: 50, nullable: true),
					UserId = table.Column<int>(nullable: false),
					SearchDate = table.Column<DateTime>(nullable: false),
					SearchTime = table.Column<string>(maxLength: 50, nullable: false),
					IsSuccess = table.Column<bool>(nullable: false),
					IsVictim = table.Column<bool>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SearchLogDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_SearchLogDetails_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ToDoTasks",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 300, nullable: true),
					UsageTypeId = table.Column<short>(nullable: true),
					DueDateTime = table.Column<DateTime>(nullable: true),
					CompletionDateTime = table.Column<DateTime>(nullable: true),
					AssignedToUserId = table.Column<int>(nullable: true),
					CreatorUserId = table.Column<int>(nullable: false),
					Description = table.Column<string>(maxLength: 500, nullable: true),
					PriorityId = table.Column<short>(nullable: true),
					StateId = table.Column<short>(nullable: true),
					EntryDateTime = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ToDoTasks", x => x.Id);
					table.ForeignKey(
						name: "FK_ToDoTasks_ApplicationUserItems_AssignedToUserId",
						column: x => x.AssignedToUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ToDoTasks_ApplicationUserItems_CreatorUserId",
						column: x => x.CreatorUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ToDoTasks_Common_Priorities_PriorityId",
						column: x => x.PriorityId,
						principalTable: "Common_Priorities",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ToDoTasks_ToDoTask_States_StateId",
						column: x => x.StateId,
						principalTable: "ToDoTask_States",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ToDoTasks_ToDoTask_UsageTypes_UsageTypeId",
						column: x => x.UsageTypeId,
						principalTable: "ToDoTask_UsageTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TopupAccountTransactionHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 50, nullable: true),
					UploadDate = table.Column<DateTime>(nullable: false),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupAccountTransactionHeaders", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupAccountTransactionHeaders_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TopupChannelTransactionHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 50, nullable: true),
					UploadDate = table.Column<DateTime>(nullable: false),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupChannelTransactionHeaders", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupChannelTransactionHeaders_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "TransactionHeaders",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					Title = table.Column<string>(maxLength: 50, nullable: true),
					Description = table.Column<string>(maxLength: 50, nullable: true),
					UploadDate = table.Column<DateTime>(nullable: false),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TransactionHeaders", x => x.Id);
					table.ForeignKey(
						name: "FK_TransactionHeaders_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AclFilesRecords",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					SourceIp = table.Column<string>(maxLength: 50, nullable: true),
					DestinationIp = table.Column<string>(maxLength: 50, nullable: true),
					Action = table.Column<string>(maxLength: 50, nullable: true),
					SourceIp2 = table.Column<string>(maxLength: 50, nullable: true),
					DestinationIp2 = table.Column<string>(maxLength: 50, nullable: true),
					SourcePort = table.Column<string>(maxLength: 50, nullable: true),
					DestinationPort = table.Column<string>(maxLength: 50, nullable: true),
					Protocol = table.Column<string>(maxLength: 50, nullable: true),
					RouterNo = table.Column<string>(maxLength: 50, nullable: true),
					RouterId = table.Column<int>(nullable: true),
					SourceAddressTypeId = table.Column<int>(nullable: true),
					DestinationAddressTypeId = table.Column<int>(nullable: true),
					AclFilesUploadId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AclFilesRecords", x => x.Id);
					table.ForeignKey(
						name: "FK_AclFilesRecords_AclFilesUpload_AclFilesUploadId",
						column: x => x.AclFilesUploadId,
						principalTable: "AclFilesUpload",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AclFilesRecords_AddressTypes_DestinationAddressTypeId",
						column: x => x.DestinationAddressTypeId,
						principalTable: "AddressTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AclFilesRecords_Routers_RouterId",
						column: x => x.RouterId,
						principalTable: "Routers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AclFilesRecords_AddressTypes_SourceAddressTypeId",
						column: x => x.SourceAddressTypeId,
						principalTable: "AddressTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "InvalidFileItems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					InvalidItemTitle = table.Column<string>(nullable: false),
					LineNumber = table.Column<int>(nullable: false),
					AclFilesUploadId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_InvalidFileItems", x => x.Id);
					table.ForeignKey(
						name: "FK_InvalidFileItems_AclFilesUpload_AclFilesUploadId",
						column: x => x.AclFilesUploadId,
						principalTable: "AclFilesUpload",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Accessibilities",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					SourceIp = table.Column<string>(maxLength: 50, nullable: true),
					DestinationIp = table.Column<string>(maxLength: 50, nullable: true),
					SourcePort = table.Column<string>(maxLength: 50, nullable: true),
					DestinationPort = table.Column<string>(maxLength: 50, nullable: true),
					Port = table.Column<string>(maxLength: 50, nullable: true),
					IsTemp = table.Column<bool>(nullable: false),
					Protocol = table.Column<string>(maxLength: 50, nullable: true),
					ProtocolsId = table.Column<int>(nullable: true),
					SourceSystemId = table.Column<int>(nullable: true),
					DestinationSystemId = table.Column<int>(nullable: true),
					ServiceId = table.Column<int>(nullable: true),
					DestinationServiceId = table.Column<int>(nullable: true),
					ActionType = table.Column<string>(maxLength: 50, nullable: true),
					ActionTypesId = table.Column<int>(nullable: true),
					RouterId = table.Column<int>(nullable: true),
					UserId = table.Column<int>(nullable: false),
					AclFilesUploadId = table.Column<int>(nullable: true),
					VersionDate = table.Column<DateTime>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Accessibilities", x => x.Id);
					table.ForeignKey(
						name: "FK_Accessibilities_AclFilesUpload_AclFilesUploadId",
						column: x => x.AclFilesUploadId,
						principalTable: "AclFilesUpload",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_ActionTypes_ActionTypesId",
						column: x => x.ActionTypesId,
						principalTable: "ActionTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Services_DestinationServiceId",
						column: x => x.DestinationServiceId,
						principalTable: "Services",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Systems_DestinationSystemId",
						column: x => x.DestinationSystemId,
						principalTable: "Systems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Protocols_ProtocolsId",
						column: x => x.ProtocolsId,
						principalTable: "Protocols",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Routers_RouterId",
						column: x => x.RouterId,
						principalTable: "Routers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Services_ServiceId",
						column: x => x.ServiceId,
						principalTable: "Services",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_Systems_SourceSystemId",
						column: x => x.SourceSystemId,
						principalTable: "Systems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Accessibilities_ApplicationUserItems_UserId",
						column: x => x.UserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "AccessibilityRequests",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					LetterNo = table.Column<string>(maxLength: 50, nullable: true),
					LetterDate = table.Column<DateTime>(nullable: true),
					SourceIp = table.Column<string>(maxLength: 50, nullable: true),
					DestinationIp = table.Column<string>(maxLength: 50, nullable: true),
					AccessStartDate = table.Column<DateTime>(nullable: true),
					AccessEndDate = table.Column<DateTime>(nullable: true),
					Description = table.Column<string>(maxLength: 250, nullable: true),
					PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
					SourceSystemId = table.Column<int>(nullable: true),
					DestinationSystemId = table.Column<int>(nullable: true),
					ServiceId = table.Column<int>(nullable: true),
					DestinationServiceId = table.Column<int>(nullable: true),
					UserDepartmentId = table.Column<int>(nullable: true),
					RequestDepartmentId = table.Column<int>(nullable: true),
					AccessibilityTypeId = table.Column<int>(nullable: true),
					RequestingUserId = table.Column<int>(nullable: true),
					ConfirmUserId = table.Column<int>(nullable: true),
					SourceAccessibilityLevelId = table.Column<int>(nullable: true),
					DestAccessibilityLevelId = table.Column<int>(nullable: true),
					SourceProtocolId = table.Column<int>(nullable: true),
					DestinationProtocolId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AccessibilityRequests", x => x.Id);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_AccessTypes_AccessibilityTypeId",
						column: x => x.AccessibilityTypeId,
						principalTable: "AccessTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_ApplicationUserItems_ConfirmUserId",
						column: x => x.ConfirmUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_AccessibilityLevels_DestAccessibilityLevelId",
						column: x => x.DestAccessibilityLevelId,
						principalTable: "AccessibilityLevels",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Protocols_DestinationProtocolId",
						column: x => x.DestinationProtocolId,
						principalTable: "Protocols",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Services_DestinationServiceId",
						column: x => x.DestinationServiceId,
						principalTable: "Services",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Systems_DestinationSystemId",
						column: x => x.DestinationSystemId,
						principalTable: "Systems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Departments_RequestDepartmentId",
						column: x => x.RequestDepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_ApplicationUserItems_RequestingUserId",
						column: x => x.RequestingUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Services_ServiceId",
						column: x => x.ServiceId,
						principalTable: "Services",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_AccessibilityLevels_SourceAccessibilityLevelId",
						column: x => x.SourceAccessibilityLevelId,
						principalTable: "AccessibilityLevels",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Protocols_SourceProtocolId",
						column: x => x.SourceProtocolId,
						principalTable: "Protocols",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Systems_SourceSystemId",
						column: x => x.SourceSystemId,
						principalTable: "Systems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_AccessibilityRequests_Departments_UserDepartmentId",
						column: x => x.UserDepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "NotificationItems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					EntryDateTime = table.Column<DateTime>(nullable: false),
					Text = table.Column<string>(maxLength: 300, nullable: true),
					CreatedByUserId = table.Column<int>(nullable: false),
					ForUserId = table.Column<int>(nullable: false),
					ToDoTaskId = table.Column<int>(nullable: true),
					IsRead = table.Column<bool>(nullable: false),
					IsSent = table.Column<bool>(nullable: false),
					CategoryId = table.Column<short>(nullable: false),
					Url = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NotificationItems", x => x.Id);
					table.ForeignKey(
						name: "FK_NotificationItems_Notification_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Notification_Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_NotificationItems_ApplicationUserItems_CreatedByUserId",
						column: x => x.CreatedByUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_NotificationItems_ApplicationUserItems_ForUserId",
						column: x => x.ForUserId,
						principalTable: "ApplicationUserItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_NotificationItems_ToDoTasks_ToDoTaskId",
						column: x => x.ToDoTaskId,
						principalTable: "ToDoTasks",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TopupAccountTransactionDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					TransactionDate = table.Column<string>(maxLength: 20, nullable: true),
					TransactionTime = table.Column<string>(maxLength: 10, nullable: true),
					TransactionType = table.Column<string>(maxLength: 10, nullable: true),
					TransactionStatus = table.Column<string>(maxLength: 10, nullable: true),
					TransactionAmountText = table.Column<string>(maxLength: 50, nullable: true),
					TransactionAmount = table.Column<string>(nullable: true),
					RefrenceCode = table.Column<string>(maxLength: 20, nullable: true),
					Extra1 = table.Column<string>(maxLength: 100, nullable: true),
					Extra2 = table.Column<string>(maxLength: 100, nullable: true),
					Extra3 = table.Column<string>(maxLength: 100, nullable: true),
					Extra4 = table.Column<string>(maxLength: 100, nullable: true),
					BranchCode = table.Column<string>(maxLength: 20, nullable: true),
					FollowupCode2 = table.Column<string>(maxLength: 50, nullable: true),
					AccountNumber = table.Column<string>(maxLength: 30, nullable: true),
					CustomerAccountNumber = table.Column<string>(maxLength: 30, nullable: true),
					FollowupCode = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupAccountTransactionDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupAccountTransactionDetails_TopupAccountTransactionHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "TopupAccountTransactionHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TopupChannelTransactionDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					FollowupCode = table.Column<string>(maxLength: 50, nullable: true),
					CustomerAccountNumber = table.Column<string>(maxLength: 50, nullable: true),
					FollowupCode2 = table.Column<string>(maxLength: 50, nullable: true),
					Extra1 = table.Column<string>(maxLength: 50, nullable: true),
					Extra2 = table.Column<string>(maxLength: 20, nullable: true),
					ChannelType = table.Column<string>(maxLength: 10, nullable: true),
					TransactionDate = table.Column<string>(maxLength: 20, nullable: true),
					MobileNumber = table.Column<string>(maxLength: 50, nullable: true),
					AmountText = table.Column<string>(maxLength: 50, nullable: true),
					TransactionAmount = table.Column<string>(nullable: true),
					TransactionStatus = table.Column<string>(maxLength: 10, nullable: true),
					OperatorName = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TopupChannelTransactionDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_TopupChannelTransactionDetails_TopupChannelTransactionHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "TopupChannelTransactionHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TransactionDetails",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					SourcePan = table.Column<string>(maxLength: 16, nullable: true),
					TargetPan = table.Column<string>(maxLength: 16, nullable: true),
					SourcePanOrgianl = table.Column<string>(maxLength: 30, nullable: true),
					TargetPanOrgianl = table.Column<string>(maxLength: 30, nullable: true),
					TransactionDate = table.Column<DateTime>(nullable: false),
					TransactionTime = table.Column<string>(maxLength: 8, nullable: true),
					Tel = table.Column<string>(maxLength: 13, nullable: true),
					Amount = table.Column<long>(nullable: false),
					IpAddress = table.Column<string>(maxLength: 50, nullable: true),
					LogDate = table.Column<DateTime>(nullable: false),
					Status = table.Column<string>(maxLength: 50, nullable: true),
					RefNumber = table.Column<string>(maxLength: 10, nullable: true),
					UserAgent = table.Column<string>(maxLength: 50, nullable: true),
					Application = table.Column<string>(maxLength: 50, nullable: true),
					HeaderId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_TransactionDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_TransactionDetails_TransactionHeaders_HeaderId",
						column: x => x.HeaderId,
						principalTable: "TransactionHeaders",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SearchLogTransactionDetail",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RecordStatus = table.Column<bool>(nullable: false),
					SearchLogDetailId = table.Column<int>(nullable: false),
					TransactionDetailId = table.Column<int>(nullable: false),
					TopupAccountTransactionDetailId = table.Column<int>(nullable: true),
					TopupChannelTransactionDetailId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SearchLogTransactionDetail", x => x.Id);
					table.ForeignKey(
						name: "FK_SearchLogTransactionDetail_SearchLogDetails_SearchLogDetailId",
						column: x => x.SearchLogDetailId,
						principalTable: "SearchLogDetails",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SearchLogTransactionDetail_TopupAccountTransactionDetails_TopupAccountTransactionDetailId",
						column: x => x.TopupAccountTransactionDetailId,
						principalTable: "TopupAccountTransactionDetails",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_SearchLogTransactionDetail_TopupChannelTransactionDetails_TopupChannelTransactionDetailId",
						column: x => x.TopupChannelTransactionDetailId,
						principalTable: "TopupChannelTransactionDetails",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_SearchLogTransactionDetail_TransactionDetails_TransactionDetailId",
						column: x => x.TransactionDetailId,
						principalTable: "TransactionDetails",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.InsertData(
				table: "ApplicationUserItems",
				columns: new[] { "Id", "AccessFailedCount", "BankBranchId", "ConcurrencyStamp", "DepartmentId", "DutyPositionId", "Email", "EmailConfirmed", "ExpertPerformance", "ExpertPerformanceToken", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonnelCode", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "PositionId", "Qualifications", "QualificationsToken", "RegDateTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { 1, 0, null, "a0c979d1-f65e-4122-b62f-78b5b8df30da", null, null, "info@test.com", false, false, null, "مدیر", "سایت", true, null, "INFO@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEFQSCRc6wVL8pu5ChTDI4xT2A5LQ2okSnHseUkzOj0SfLwNzOdHLlhSHaf+lR3jv9A==", null, null, false, null, null, false, null, new DateTime(2021, 6, 21, 16, 46, 54, 833, DateTimeKind.Local).AddTicks(5229), "68bccbf3-564b-4f50-b58e-def000a99746", false, "admin" });

			migrationBuilder.InsertData(
				table: "ApplicationUser_OrganizationalCharts",
				columns: new[] { "Id", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ (short)3, false, "کارشناس مسئول" },
					{ (short)4, false, "کارشناس" },
					{ (short)2, false, "معاون" },
					{ (short)1, false, "رئیس" }
				});

			migrationBuilder.InsertData(
				table: "ApplicationUser_Roles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "PanelMenu" },
				values: new object[,]
				{
					{ 1, "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d", "developer", "DEVELOPER", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" },
					{ 5, "cf1a3d91-b47f-45f4-be10-1c8d4fc6af0b", "امور شعب", "امور شعب", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" },
					{ 4, "fdf378ee-ff1d-4594-a5f3-27ae72e25a28", "اداره پشتیبانی", "اداره پشتیبانی", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" },
					{ 3, "a1a62ada-65d4-42ef-ac91-faf6f10ca025", "اداره حفاظت", "اداره حفاظت", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" },
					{ 2, "a1a62ada-65d4-42ef-ac91-faf6f10ca025", "admin", "ADMIN", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" },
					{ 6, "4aa334bb-5c36-4b69-a74b-3f0d826538b7", "کاربر گزارش", "کاربر گزارش", "[{\"Id\":\"li-accessibility\",\"Text\":\"تعریف دسترسی\",\"Link\":\"/Accessibilities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessibilityRequest\",\"Text\":\"آرشیو درخواست دسترسی\",\"Link\":\"/AccessibilityRequests\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-systems\",\"Text\":\"سامانه ها\",\"Link\":\"/Systems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-aclFilesUpload\",\"Text\":\"بارگذاری فایل\",\"Link\":\"/AclFilesUploads\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-policy\",\"Text\":\"قوانین\",\"Link\":\"/Policies\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-report\",\"Text\":\"گزارشات\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-reportByFilter\",\"Text\":\"گزارش بر اساس پارامتر\",\"Link\":\"/Reports/ReportByFilter\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-invalidFilesItem\",\"Text\":\"گزارش استثنایات\",\"Link\":\"/InvalidFileItems\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-reportByCount\",\"Text\":\"گزارش دسترسی بر اساس تعداد\",\"Link\":\"/Reports/ReportByCount\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-networkDiagram\",\"Text\":\"گراف دسترسی\",\"Link\":\"/NetworkDiagrams\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transaction\",\"Text\":\"تراکنش ها\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/ExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/Transactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/Transactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-searchLog\",\"Text\":\"جستجو در تراکنش ها\",\"Link\":\"/SearchLogs/SearchLog\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-abundanceReport \",\"Text\":\"گزارش فراوانی\",\"Link\":\"/SearchLogs/AbundanceReport\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-sajayaReport \",\"Text\":\"گزارش سجایا\",\"Link\":\"/SearchLogs/SajayaReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های حساب\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupAccountExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupAccountTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupAccountTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-transaction\",\"Text\":\"Topup تراکنش های کانال\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-excelFormat\",\"Text\":\"تعریف فرمت اکسل\",\"Link\":\"/TopupChannelExcelFormats\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionList\",\"Text\":\"بارگذاری فایل تراکنش\",\"Link\":\"/TopupChannelTransactions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-transactionReport\",\"Text\":\"گزارش تراکنش ها\",\"Link\":\"/TopupChannelTransactions/TransactionDetailReport\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-base\",\"Text\":\"اطلاعات پایه\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-accessType\",\"Text\":\"نوع دسترسی\",\"Link\":\"/AccessTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-addressType\",\"Text\":\"نوع آدرس\",\"Link\":\"/AddressTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-department\",\"Text\":\"دپارتمان\",\"Link\":\"/Departments\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-protocol\",\"Text\":\"پروتکل\",\"Link\":\"/Protocols\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-service\",\"Text\":\"پورت(سرویس)\",\"Link\":\"/Services\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-serviceGroup\",\"Text\":\"گروه سرویس\",\"Link\":\"/ServiceGroups\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-actionType\",\"Text\":\"عملیات\",\"Link\":\"/ActionTypes\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-router\",\"Text\":\"روتر\",\"Link\":\"/Routers\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-accessRange\",\"Text\":\"محدوده دسترسی\",\"Link\":\"/AccessRanges\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-authorities\",\"Text\":\"اختیار\",\"Link\":\"/Authorities\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-dutyPositions\",\"Text\":\"محل خدمت\",\"Link\":\"/DutyPositions\",\"Icon\":null,\"Items\":null}]},{\"Id\":\"li-account\",\"Text\":\"ناحیه کاربری\",\"Link\":null,\"Icon\":\"flaticon-list-3\",\"Items\":[{\"Id\":\"li-users\",\"Text\":\"کاربران\",\"Link\":\"/UserManagement\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-roles\",\"Text\":\"نقش\",\"Link\":\"/AccessRights\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-position\",\"Text\":\"پست سازمانی\",\"Link\":\"/Positions\",\"Icon\":null,\"Items\":null},{\"Id\":\"li-changePassword\",\"Text\":\"تغییر کلمه عبور\",\"Link\":\"/UserManagement/ChangePassword\",\"Icon\":null,\"Items\":null}]}]" }
				});

			migrationBuilder.InsertData(
				table: "BankBranches",
				columns: new[] { "Id", "BranchHeadId", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ 514, null, false, "اداره امور شعب آذربایجان غربی" },
					{ 642, null, false, "اداره امور شعب بوشهر" },
					{ 501, null, false, "اداره امور شعب آذربایجان شرقی" },
					{ 502, null, false, "اداره امور شعب اصفهان" },
					{ 505, null, false, "اداره امور شعب کرمانشاه" },
					{ 506, null, false, "اداره امور شعب خوزستان" },
					{ 508, null, false, "اداره امور شعب مرکزی" },
					{ 518, null, false, "اداره امور شعب اردبیل" },
					{ 525, null, false, "اداره امور شعب زنجان" },
					{ 521, null, false, "اداره امور شعب کردستان" },
					{ 522, null, false, "اداره امور شعب همدان" },
					{ 523, null, false, "اداره امور شعب لرستان" },
					{ 624, null, false, "اداره امور شعب جنوبی" },
					{ 526, null, false, "اداره امور شعب البرز" },
					{ 750, null, false, "اداره امور شعب ایلام" },
					{ 21, null, false, "اداره خزانه داری" },
					{ 520, null, false, "اداره امور شعب قزوین" },
					{ 622, null, false, "اداره امور شعب خراسان شمالی" },
					{ 527, null, false, "اداره امور شعب قم" },
					{ 528, null, false, "اداره امور شعب چهارمحال بختیاری" },
					{ 529, null, false, "اداره امور شعب کهگلویه و بویراحمد" },
					{ 515, null, false, "اداره منطقه یک تهران" },
					{ 516, null, false, "اداره منطقه دو تهران" },
					{ 510, null, false, "اداره منطقه سه تهران" },
					{ 164, null, false, "صندوق توسعه ملی" },
					{ 101, null, false, "شعبه مستقل مرکزی" },
					{ 500, null, false, "اداره امور شعب خراسان رضوی" },
					{ 503, null, false, "اداره امور شعب فارس" },
					{ 107, null, false, "شعبه مستقل بلوار" },
					{ 507, null, false, "اداره امور شعب مازندران" },
					{ 509, null, false, "اداره امور شعب کرمان" },
					{ 512, null, false, "اداره امور شعب سیستان و بلوچستان" },
					{ 524, null, false, "اداره امور شعب هرمزگان" },
					{ 513, null, false, "اداره امور شعب یزد" },
					{ 517, null, false, "اداره امور شعب سمنان" },
					{ 519, null, false, "اداره امور شعب گلستان" },
					{ 504, null, false, "اداره امور شعب گیلان" }
				});

			migrationBuilder.InsertData(
				table: "Common_Priorities",
				columns: new[] { "Id", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ (short)3, false, "پایین" },
					{ (short)2, false, "معمولی" },
					{ (short)1, false, "ضروری" }
				});

			migrationBuilder.InsertData(
				table: "Common_TestOptions",
				columns: new[] { "Id", "RecordStatus", "TableId", "Title" },
				values: new object[,]
				{
					{ (short)16, false, (short)6, "None" },
					{ (short)18, false, (short)6, "Complete" },
					{ (short)17, false, (short)6, "Partial" },
					{ (short)15, false, (short)5, "Complete" },
					{ (short)20, false, (short)7, "Partial" },
					{ (short)19, false, (short)7, "None" },
					{ (short)21, false, (short)7, "Complete" },
					{ (short)28, false, (short)9, "عدم تایید" },
					{ (short)23, false, (short)8, "High" },
					{ (short)24, false, (short)8, "Medium" },
					{ (short)25, false, (short)8, "Low" },
					{ (short)26, false, (short)8, "Information" },
					{ (short)27, false, (short)9, "تایید" },
					{ (short)14, false, (short)5, "Partial" },
					{ (short)29, false, (short)9, "امکان تست وجود ندارد" },
					{ (short)22, false, (short)8, "Critical" },
					{ (short)13, false, (short)5, "None" },
					{ (short)5, false, (short)2, "Local-Adjacent Network" },
					{ (short)11, false, (short)4, "Single" },
					{ (short)10, false, (short)4, "Multiple" },
					{ (short)9, false, (short)3, "Low" },
					{ (short)8, false, (short)3, "Medium" },
					{ (short)7, false, (short)3, "High" },
					{ (short)6, false, (short)2, "Network" },
					{ (short)4, false, (short)1, "Not Accessible" },
					{ (short)3, false, (short)1, "Failed" },
					{ (short)2, false, (short)1, "Passes" },
					{ (short)1, false, (short)1, "Not Applicable" },
					{ (short)12, false, (short)4, "None" }
				});

			migrationBuilder.InsertData(
				table: "Notification_Categories",
				columns: new[] { "Id", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ (short)4, false, "موعد انجام تست" },
					{ (short)3, false, "موعد انجام وظیفه" },
					{ (short)2, false, "تغییر وضعیت وظیفه" },
					{ (short)1, false, "وظیفه ارسالی" }
				});

			migrationBuilder.InsertData(
				table: "Settings",
				columns: new[] { "Id", "EmailPassword", "EmailPortNumber", "EmailSmtpServer", "EmailUsername", "EnableSsl", "RecordStatus", "SmsPassword", "SmsServiceNumber", "SmsUserName", "ThanksMsg", "WebSiteTitle", "WelcomeText" },
				values: new object[] { (short)1, null, null, null, null, null, false, null, null, null, null, "سامانه تست نفوذ", null });

			migrationBuilder.InsertData(
				table: "ToDoTask_States",
				columns: new[] { "Id", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ (short)1, false, "شروع نشده" },
					{ (short)2, false, "در حال انجام" },
					{ (short)3, false, "انجام شد" }
				});

			migrationBuilder.InsertData(
				table: "ToDoTask_UsageTypes",
				columns: new[] { "Id", "RecordStatus", "Title" },
				values: new object[,]
				{
					{ (short)1, false, "تماس تلفنی" },
					{ (short)2, false, "جلسه" },
					{ (short)3, false, "یادآوری" }
				});

			migrationBuilder.InsertData(
				table: "ApplicationUser_RoleClaims",
				columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
				values: new object[,]
				{
					{ 1, "Permission", "AccessRights_Index", 1 },
					{ 2, "Permission", "AccessRights_GetDetail", 1 },
					{ 3, "Permission", "AccessRights_AddDetail", 1 },
					{ 4, "Permission", "AccessRights_EditDetail", 1 },
					{ 5, "Permission", "AccessRights_DeleteRows", 1 }
				});

			migrationBuilder.InsertData(
				table: "ApplicationUser_UserRoles",
				columns: new[] { "UserId", "RoleId" },
				values: new object[] { 1, 2 });

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_AclFilesUploadId",
				table: "Accessibilities",
				column: "AclFilesUploadId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_ActionTypesId",
				table: "Accessibilities",
				column: "ActionTypesId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_DestinationServiceId",
				table: "Accessibilities",
				column: "DestinationServiceId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_DestinationSystemId",
				table: "Accessibilities",
				column: "DestinationSystemId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_ProtocolsId",
				table: "Accessibilities",
				column: "ProtocolsId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_RouterId",
				table: "Accessibilities",
				column: "RouterId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_ServiceId",
				table: "Accessibilities",
				column: "ServiceId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_SourceSystemId",
				table: "Accessibilities",
				column: "SourceSystemId");

			migrationBuilder.CreateIndex(
				name: "IX_Accessibilities_UserId",
				table: "Accessibilities",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_AccessibilityTypeId",
				table: "AccessibilityRequests",
				column: "AccessibilityTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_ConfirmUserId",
				table: "AccessibilityRequests",
				column: "ConfirmUserId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_DestAccessibilityLevelId",
				table: "AccessibilityRequests",
				column: "DestAccessibilityLevelId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_DestinationProtocolId",
				table: "AccessibilityRequests",
				column: "DestinationProtocolId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_DestinationServiceId",
				table: "AccessibilityRequests",
				column: "DestinationServiceId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_DestinationSystemId",
				table: "AccessibilityRequests",
				column: "DestinationSystemId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_RequestDepartmentId",
				table: "AccessibilityRequests",
				column: "RequestDepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_RequestingUserId",
				table: "AccessibilityRequests",
				column: "RequestingUserId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_ServiceId",
				table: "AccessibilityRequests",
				column: "ServiceId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_SourceAccessibilityLevelId",
				table: "AccessibilityRequests",
				column: "SourceAccessibilityLevelId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_SourceProtocolId",
				table: "AccessibilityRequests",
				column: "SourceProtocolId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_SourceSystemId",
				table: "AccessibilityRequests",
				column: "SourceSystemId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessibilityRequests_UserDepartmentId",
				table: "AccessibilityRequests",
				column: "UserDepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_AccessRangeDetails_AccessRangeHeaderId",
				table: "AccessRangeDetails",
				column: "AccessRangeHeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_AclFilesRecords_AclFilesUploadId",
				table: "AclFilesRecords",
				column: "AclFilesUploadId");

			migrationBuilder.CreateIndex(
				name: "IX_AclFilesRecords_DestinationAddressTypeId",
				table: "AclFilesRecords",
				column: "DestinationAddressTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_AclFilesRecords_RouterId",
				table: "AclFilesRecords",
				column: "RouterId");

			migrationBuilder.CreateIndex(
				name: "IX_AclFilesRecords_SourceAddressTypeId",
				table: "AclFilesRecords",
				column: "SourceAddressTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_AclFilesUpload_RouterId",
				table: "AclFilesUpload",
				column: "RouterId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationRoleAuthorities_ApplicationRoleId",
				table: "ApplicationRoleAuthorities",
				column: "ApplicationRoleId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationRoleAuthorities_AuthorityId",
				table: "ApplicationRoleAuthorities",
				column: "AuthorityId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUser_ActivityLogs_UserId",
				table: "ApplicationUser_ActivityLogs",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUser_Claims_UserId",
				table: "ApplicationUser_Claims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUser_Logins_UserId",
				table: "ApplicationUser_Logins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUser_RoleClaims_RoleId",
				table: "ApplicationUser_RoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "ApplicationUser_Roles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUser_UserRoles_RoleId",
				table: "ApplicationUser_UserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserAccessRageHeaders_AccessRangeHeaderId",
				table: "ApplicationUserAccessRageHeaders",
				column: "AccessRangeHeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserAccessRageHeaders_ApplicationUserId",
				table: "ApplicationUserAccessRageHeaders",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_BankBranchId",
				table: "ApplicationUserItems",
				column: "BankBranchId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_DepartmentId",
				table: "ApplicationUserItems",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_DutyPositionId",
				table: "ApplicationUserItems",
				column: "DutyPositionId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "ApplicationUserItems",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "ApplicationUserItems",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_ApplicationUserItems_PositionId",
				table: "ApplicationUserItems",
				column: "PositionId");

			migrationBuilder.CreateIndex(
				name: "IX_BankBranches_BranchHeadId",
				table: "BankBranches",
				column: "BranchHeadId");

			migrationBuilder.CreateIndex(
				name: "IX_ExcelFormatDetails_HeaderId",
				table: "ExcelFormatDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_InvalidFileItems_AclFilesUploadId",
				table: "InvalidFileItems",
				column: "AclFilesUploadId");

			migrationBuilder.CreateIndex(
				name: "IX_NotificationItems_CategoryId",
				table: "NotificationItems",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_NotificationItems_CreatedByUserId",
				table: "NotificationItems",
				column: "CreatedByUserId");

			migrationBuilder.CreateIndex(
				name: "IX_NotificationItems_ForUserId",
				table: "NotificationItems",
				column: "ForUserId");

			migrationBuilder.CreateIndex(
				name: "IX_NotificationItems_ToDoTaskId",
				table: "NotificationItems",
				column: "ToDoTaskId");

			migrationBuilder.CreateIndex(
				name: "IX_Policies_ProtocolId",
				table: "Policies",
				column: "ProtocolId");

			migrationBuilder.CreateIndex(
				name: "IX_SearchLogDetails_UserId",
				table: "SearchLogDetails",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_SearchLogTransactionDetail_SearchLogDetailId",
				table: "SearchLogTransactionDetail",
				column: "SearchLogDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_SearchLogTransactionDetail_TopupAccountTransactionDetailId",
				table: "SearchLogTransactionDetail",
				column: "TopupAccountTransactionDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_SearchLogTransactionDetail_TopupChannelTransactionDetailId",
				table: "SearchLogTransactionDetail",
				column: "TopupChannelTransactionDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_SearchLogTransactionDetail_TransactionDetailId",
				table: "SearchLogTransactionDetail",
				column: "TransactionDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_Services_ProtocolId",
				table: "Services",
				column: "ProtocolId");

			migrationBuilder.CreateIndex(
				name: "IX_Services_ServiceLevelId",
				table: "Services",
				column: "ServiceLevelId");

			migrationBuilder.CreateIndex(
				name: "IX_Systems_AccessibilityLevelId",
				table: "Systems",
				column: "AccessibilityLevelId");

			migrationBuilder.CreateIndex(
				name: "IX_Systems_DepartmentId",
				table: "Systems",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Systems_InformationAccessibilityLevelId",
				table: "Systems",
				column: "InformationAccessibilityLevelId");

			migrationBuilder.CreateIndex(
				name: "IX_ToDoTasks_AssignedToUserId",
				table: "ToDoTasks",
				column: "AssignedToUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ToDoTasks_CreatorUserId",
				table: "ToDoTasks",
				column: "CreatorUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ToDoTasks_PriorityId",
				table: "ToDoTasks",
				column: "PriorityId");

			migrationBuilder.CreateIndex(
				name: "IX_ToDoTasks_StateId",
				table: "ToDoTasks",
				column: "StateId");

			migrationBuilder.CreateIndex(
				name: "IX_ToDoTasks_UsageTypeId",
				table: "ToDoTasks",
				column: "UsageTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupAccountExcelFormatDetails_HeaderId",
				table: "TopupAccountExcelFormatDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupAccountTransactionDetails_HeaderId",
				table: "TopupAccountTransactionDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupAccountTransactionHeaders_UserId",
				table: "TopupAccountTransactionHeaders",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupChannelExcelFormatDetails_HeaderId",
				table: "TopupChannelExcelFormatDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupChannelTransactionDetails_HeaderId",
				table: "TopupChannelTransactionDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_TopupChannelTransactionHeaders_UserId",
				table: "TopupChannelTransactionHeaders",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionDetails_HeaderId",
				table: "TransactionDetails",
				column: "HeaderId");

			migrationBuilder.CreateIndex(
				name: "IX_TransactionHeaders_UserId",
				table: "TransactionHeaders",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Accessibilities");

			migrationBuilder.DropTable(
				name: "AccessibilityRequests");

			migrationBuilder.DropTable(
				name: "AccessRangeDetails");

			migrationBuilder.DropTable(
				name: "AclFilesRecords");

			migrationBuilder.DropTable(
				name: "ApplicationRoleAuthorities");

			migrationBuilder.DropTable(
				name: "ApplicationUser_ActivityLogs");

			migrationBuilder.DropTable(
				name: "ApplicationUser_Claims");

			migrationBuilder.DropTable(
				name: "ApplicationUser_Logins");

			migrationBuilder.DropTable(
				name: "ApplicationUser_OrganizationalCharts");

			migrationBuilder.DropTable(
				name: "ApplicationUser_RoleClaims");

			migrationBuilder.DropTable(
				name: "ApplicationUser_Tokens");

			migrationBuilder.DropTable(
				name: "ApplicationUser_UserRoles");

			migrationBuilder.DropTable(
				name: "ApplicationUserAccessRageHeaders");

			migrationBuilder.DropTable(
				name: "Common_TestOptions");

			migrationBuilder.DropTable(
				name: "ExcelFormatDetails");

			migrationBuilder.DropTable(
				name: "InvalidFileItems");

			migrationBuilder.DropTable(
				name: "NotificationItems");

			migrationBuilder.DropTable(
				name: "Policies");

			migrationBuilder.DropTable(
				name: "Report_Boxes");

			migrationBuilder.DropTable(
				name: "Report_Charts");

			migrationBuilder.DropTable(
				name: "SearchLogTransactionDetail");

			migrationBuilder.DropTable(
				name: "ServiceGroups");

			migrationBuilder.DropTable(
				name: "Settings");

			migrationBuilder.DropTable(
				name: "TopupAccountExcelFormatDetails");

			migrationBuilder.DropTable(
				name: "TopupChannelExcelFormatDetails");

			migrationBuilder.DropTable(
				name: "ActionTypes");

			migrationBuilder.DropTable(
				name: "AccessTypes");

			migrationBuilder.DropTable(
				name: "Services");

			migrationBuilder.DropTable(
				name: "Systems");

			migrationBuilder.DropTable(
				name: "AddressTypes");

			migrationBuilder.DropTable(
				name: "Authorities");

			migrationBuilder.DropTable(
				name: "ApplicationUser_Roles");

			migrationBuilder.DropTable(
				name: "AccessRangeHeaders");

			migrationBuilder.DropTable(
				name: "ExcelFormatHeaders");

			migrationBuilder.DropTable(
				name: "AclFilesUpload");

			migrationBuilder.DropTable(
				name: "Notification_Categories");

			migrationBuilder.DropTable(
				name: "ToDoTasks");

			migrationBuilder.DropTable(
				name: "SearchLogDetails");

			migrationBuilder.DropTable(
				name: "TopupAccountTransactionDetails");

			migrationBuilder.DropTable(
				name: "TopupChannelTransactionDetails");

			migrationBuilder.DropTable(
				name: "TransactionDetails");

			migrationBuilder.DropTable(
				name: "TopupAccountExcelFormatHeaders");

			migrationBuilder.DropTable(
				name: "TopupChannelExcelFormatHeaders");

			migrationBuilder.DropTable(
				name: "Protocols");

			migrationBuilder.DropTable(
				name: "ServiceLevels");

			migrationBuilder.DropTable(
				name: "AccessibilityLevels");

			migrationBuilder.DropTable(
				name: "Routers");

			migrationBuilder.DropTable(
				name: "Common_Priorities");

			migrationBuilder.DropTable(
				name: "ToDoTask_States");

			migrationBuilder.DropTable(
				name: "ToDoTask_UsageTypes");

			migrationBuilder.DropTable(
				name: "TopupAccountTransactionHeaders");

			migrationBuilder.DropTable(
				name: "TopupChannelTransactionHeaders");

			migrationBuilder.DropTable(
				name: "TransactionHeaders");

			migrationBuilder.DropTable(
				name: "ApplicationUserItems");

			migrationBuilder.DropTable(
				name: "BankBranches");

			migrationBuilder.DropTable(
				name: "Departments");

			migrationBuilder.DropTable(
				name: "DutyPositions");

			migrationBuilder.DropTable(
				name: "Positions");
		}
	}
}
