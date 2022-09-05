using ApplicationCommon;
using DomainEntities.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
	{
		public void Configure(EntityTypeBuilder<ApplicationRole> builder)
		{
			var adminMenu = new List<UserPanelMenu>
			{
				new UserPanelMenu { Text = "تعریف دسترسی", Id="li-accessibility", Link = "/Accessibilities", Items = null },
				new UserPanelMenu { Text = "آرشیو درخواست دسترسی", Id="li-accessibilityRequest", Link = "/AccessibilityRequests", Items = null },
				new UserPanelMenu { Text = "سامانه ها", Id="li-systems", Link = "/Systems", Items = null },
				new UserPanelMenu { Text = "بارگذاری فایل", Id="li-aclFilesUpload", Link = "/AclFilesUploads", Items = null },
				new UserPanelMenu { Text = "قوانین", Id="li-policy", Link = "/Policies", Items = null },
				new UserPanelMenu { Text = "گزارشات", Id="li-report", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "گزارش بر اساس پارامتر",Id="li-reportByFilter", Link = "/Reports/ReportByFilter", Items = null },
				new UserPanelMenu { Text = "گزارش استثنایات",Id="li-invalidFilesItem", Link = "/InvalidFileItems", Items = null },
				new UserPanelMenu { Text = "گزارش دسترسی بر اساس تعداد",Id="li-reportByCount", Link = "/Reports/ReportByCount", Items = null },
					}
				},
				new UserPanelMenu { Text = "گراف دسترسی",Id="li-networkDiagram", Link = "/NetworkDiagrams", Items = null },
				new UserPanelMenu { Text = "تراکنش ها", Id="li-transaction", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "تعریف فرمت اکسل",Id="li-excelFormat", Link = "/ExcelFormats", Items = null },
				new UserPanelMenu { Text = "بارگذاری فایل تراکنش",Id="li-transactionList", Link = "/Transactions", Items = null },
				new UserPanelMenu { Text = "گزارش تراکنش ها",Id="li-transactionReport", Link = "/Transactions/TransactionDetailReport", Items = null },
				new UserPanelMenu { Text = "جستجو در تراکنش ها",Id="li-searchLog", Link = "/SearchLogs/SearchLog", Items = null },
				new UserPanelMenu { Text = "گزارش فراوانی",Id="li-abundanceReport ", Link = "/SearchLogs/AbundanceReport", Items = null },
				new UserPanelMenu { Text = "گزارش سجایا",Id="li-sajayaReport ", Link = "/SearchLogs/SajayaReport", Items = null },
					}
				},
				new UserPanelMenu { Text = "Topup تراکنش های حساب", Id="li-transaction", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "تعریف فرمت اکسل",Id="li-excelFormat", Link = "/TopupAccountExcelFormats", Items = null },
				new UserPanelMenu { Text = "بارگذاری فایل تراکنش",Id="li-transactionList", Link = "/TopupAccountTransactions", Items = null },
				new UserPanelMenu { Text = "گزارش تراکنش ها",Id="li-transactionReport", Link = "/TopupAccountTransactions/TransactionDetailReport", Items = null },
					}
				},
				new UserPanelMenu { Text = "Topup تراکنش های کانال", Id="li-transaction", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "تعریف فرمت اکسل",Id="li-excelFormat", Link = "/TopupChannelExcelFormats", Items = null },
				new UserPanelMenu { Text = "بارگذاری فایل تراکنش",Id="li-transactionList", Link = "/TopupChannelTransactions", Items = null },
				new UserPanelMenu { Text = "گزارش تراکنش ها",Id="li-transactionReport", Link = "/TopupChannelTransactions/TransactionDetailReport", Items = null },
					}
				},
				new UserPanelMenu { Text = "اطلاعات پایه", Id="li-base", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "نوع دسترسی",Id="li-accessType", Link = "/AccessTypes", Items = null },
				new UserPanelMenu { Text = "نوع آدرس",Id="li-addressType", Link = "/AddressTypes", Items = null },
				new UserPanelMenu { Text = "دپارتمان",Id="li-department", Link = "/Departments", Items = null },
				new UserPanelMenu { Text = "پروتکل",Id="li-protocol", Link = "/Protocols", Items = null },
				new UserPanelMenu { Text = "پورت(سرویس)", Id="li-service", Link = "/Services", Items = null },
				new UserPanelMenu { Text = "گروه سرویس", Id="li-serviceGroup", Link = "/ServiceGroups", Items = null },
				new UserPanelMenu { Text = "عملیات", Id="li-actionType", Link = "/ActionTypes", Items = null },
				new UserPanelMenu { Text = "روتر", Id="li-router", Link = "/Routers", Items = null },
				new UserPanelMenu { Text = "محدوده دسترسی", Id="li-accessRange", Link = "/AccessRanges", Items = null },
				new UserPanelMenu { Text = "اختیار", Id="li-authorities", Link = "/Authorities", Items = null },
				new UserPanelMenu { Text = "محل خدمت", Id="li-dutyPositions", Link = "/DutyPositions", Items = null },
					}
				},

				new UserPanelMenu { Text = "ناحیه کاربری", Id="li-account", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
				new UserPanelMenu { Text = "کاربران",Id="li-users", Link = "/UserManagement", Items = null },
				new UserPanelMenu { Text = "نقش",Id="li-roles", Link = "/AccessRights", Items = null },
				new UserPanelMenu { Text = "پست سازمانی",Id="li-position", Link = "/Positions", Items = null },
				new UserPanelMenu { Text = "تغییر کلمه عبور",Id="li-changePassword", Link = "/UserManagement/ChangePassword", Items = null },
					}
				}
			};

			builder.ToTable("ApplicationUser_Roles");
			builder.HasData(new ApplicationRole
			{
				Id = (int)ApplicationRolesEnum.Developer,
				Name = ApplicationRolesEnum.Developer.DescriptionAttr(),
				NormalizedName = ApplicationRolesEnum.Developer.DescriptionAttr().ToUpper(),
				ConcurrencyStamp = "15d0ff7d-57e4-43cc-a3bd-7c87d7b9be7d",
				PanelMenu = JsonConvert.SerializeObject(adminMenu)
			},
				new ApplicationRole
				{
					Id = (int)ApplicationRolesEnum.Admin,
					Name = ApplicationRolesEnum.Admin.DescriptionAttr(),
					NormalizedName = ApplicationRolesEnum.Admin.DescriptionAttr().ToUpper(),
					ConcurrencyStamp = "a1a62ada-65d4-42ef-ac91-faf6f10ca025",
					PanelMenu = JsonConvert.SerializeObject(adminMenu)
				},

				new ApplicationRole
				{
					Id = (int)ApplicationRolesEnum.ProtectionOffice,
					Name = ApplicationRolesEnum.ProtectionOffice.DescriptionAttr(),
					NormalizedName = ApplicationRolesEnum.ProtectionOffice.DescriptionAttr().ToUpper(),
					ConcurrencyStamp = "a1a62ada-65d4-42ef-ac91-faf6f10ca025",
					PanelMenu = JsonConvert.SerializeObject(adminMenu)
				},

				new ApplicationRole
				{
					Id = (int)ApplicationRolesEnum.SupportOffice,
					Name = ApplicationRolesEnum.SupportOffice.DescriptionAttr(),
					NormalizedName = ApplicationRolesEnum.SupportOffice.DescriptionAttr().ToUpper(),
					ConcurrencyStamp = "fdf378ee-ff1d-4594-a5f3-27ae72e25a28",
					PanelMenu = JsonConvert.SerializeObject(adminMenu)
				},

				new ApplicationRole
				{
					Id = (int)ApplicationRolesEnum.BranchHead,
					Name = ApplicationRolesEnum.BranchHead.DescriptionAttr(),
					NormalizedName = ApplicationRolesEnum.BranchHead.DescriptionAttr().ToUpper(),
					ConcurrencyStamp = "cf1a3d91-b47f-45f4-be10-1c8d4fc6af0b",
					PanelMenu = JsonConvert.SerializeObject(adminMenu)
				},

				new ApplicationRole
				{
					Id = (int)ApplicationRolesEnum.ReportUser,
					Name = ApplicationRolesEnum.ReportUser.DescriptionAttr(),
					NormalizedName = ApplicationRolesEnum.ReportUser.DescriptionAttr().ToUpper(),
					ConcurrencyStamp = "4aa334bb-5c36-4b69-a74b-3f0d826538b7",
					PanelMenu = JsonConvert.SerializeObject(adminMenu)
				});
		}
	}
}