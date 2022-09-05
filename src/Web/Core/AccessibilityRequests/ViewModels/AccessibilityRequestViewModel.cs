using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.AccessibilityRequests.ViewModels
{
	public class AccessibilityRequestViewModel
	{
		[HiddenInput]
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "شماره نامه")]
		public string LetterNo { get; set; }

		[Display(Name = "تاریخ نامه")]
		[Required(ErrorMessage = "{0} را انتخاب نمایید")]
		[RegularExpression(@"\d{4,4}/\d{2,2}/\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string LetterDate { get; set; }

		[Display(Name = "آدرس سیستم مبدا")]
		public string SourceIp { get; set; }

		[Display(Name = "ادرس سیستم مقصد")]
		public string DestinationIp { get; set; }

		[Display(Name = "تاریخ شروع دسترسی")]
		[Required(ErrorMessage = "{0} را انتخاب نمایید")]
		[RegularExpression(@"\d{4,4}/\d{2,2}/\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string AccessStartDate { get; set; }

		[Display(Name = "تاریخ پایان دسترسی")]
		[Required(ErrorMessage = "{0} را انتخاب نمایید")]
		[RegularExpression(@"\d{4,4}/\d{2,2}/\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string AccessEndDate { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "تلفن تماس کارشناس")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "واحد درخواست کننده")]
		public int? RequestDepartmentId { get; set; }
		public string RequestDepartmentTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "سامانه مبدا")]
		public int? SourceSystemId { get; set; }
		public string SourceSystemTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "سامانه مقصد")]
		public int? DestinationSystemId { get; set; }
		public string DestinationSystemTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پورت مبدا")]
		public int? ServiceId { get; set; }
		public string ServiceTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پورت مقصد")]
		public int? DestinationServiceId { get; set; }
		public string DestinationServiceTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "واحد بهره بردار")]
		public int? UserDepartmentId { get; set; }
		public string UserDepartmentTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "کاربر درخواست دهنده")]
		public int? RequestingUserId { get; set; }
		public string RequestingUser { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "کاربر تایید کننده")]
		public int? ConfirmUserId { get; set; }
		public string ConfirmUser { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "طبقه بندی اطلاعات مبدا")]
		public int? SourceAccessibilityLevelId { get; set; }
		public string SourceAccessibilityLevelTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "طبقه بندی اطلاعات مقصد")]
		public int? DestAccessibilityLevelId { get; set; }
		public string DestAccessibilityLevelTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پروتکل مبدا")]
		public int? SourceProtocolId { get; set; }
		public string SourceProtocolTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پروتکل مقصد")]
		public int? DestinationProtocolId { get; set; }
		public string DestinationProtocolTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نوع درخواست")]
		public int? AccessibilityTypeId { get; set; }
		public string AccessibilityTypeTitle { get; set; }

		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "ضریب اهمیت کاربر")]
		public int ImportanceFactor { get; set; }

		public SelectList SystemList { get; set; }
		public SelectList ProtocolList { get; set; }
		public SelectList AccessibilityLeveList { get; set; }
		public SelectList ServiceList { get; set; }
		public SelectList UserList { get; set; }
		public SelectList DepartmentList { get; set; }
		public SelectList AccessibilityTypeList { get; set; }
		public object SystemIpList { get; set; }
	}
}
