using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Reports.ViewModels
{
	public class SearchViewModel
	{
		[Display(Name = "سامانه مبدا")]
		public int? SourceSystemId { get; set; }
		[Display(Name = "سامانه مقصد")]
		public int? DestinationSystemId { get; set; }
		[Display(Name = "آدرس سامانه مبدا")]
		public string SourceIp { get; set; }
		[Display(Name = "آدرس سامانه مقصد")]
		public string DestinationIp { get; set; }
		[Display(Name = "پورت مبدا")]
		public int? ServiceId { get; set; }
		[Display(Name = "پورت مقصد")]
		public int? DestinationServiceId { get; set; }
		[Display(Name = "کاربر درخواست دهنده")]
		public int? RequestingUserId { get; set; }
		[Display(Name = "کاربر تایید کننده")]
		public int? ConfirmUserId { get; set; }
		[Display(Name = "واحد کاربر")]
		public int? UserDepartmentId { get; set; }
		[Display(Name = "واحد درخواست دهنده")]
		public int? RequestDepartmentId { get; set; }
		[Display(Name = "نوع درخواست")]
		public int? AccessibilityTypeId { get; set; }
		public short? Search { get; set; } = null;
		public SelectList SystemList { get; set; }
		public SelectList AccessibilityLeveList { get; set; }
		public SelectList ServiceList { get; set; }
		public SelectList UserList { get; set; }
		public SelectList DepartmentList { get; set; }
		public SelectList AccessibilityTypeList { get; set; }
	}
}
