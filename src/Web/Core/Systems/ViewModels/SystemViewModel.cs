using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Systems.ViewModels
{
	public class SystemViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string SystemName { get; set; }
		[RegularExpression("^([0-9]{1,3}\\.){3}[0-9]{1,3}$", ErrorMessage = "فرمت {0} را درست وارد نمایید")]
		[Display(Name = "آدرس شبکه")]
		public string IpAddress { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "دسترسی پذیری")]
		public int? AccessibilityLevelId { get; set; }
		public string AccessibilityLevelTitle { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "دسترسی پذیری اطلاعاتی")]
		public int? InformationAccessibilityLevelId { get; set; }
		public string InformationAccessibilityLevelTitle { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نوع")]
		public int KindId { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "ضریب اهمیت")]
		public int? ImportanceFactor { get; set; }
		[Display(Name = "کد پرسنلی")]
		public int? PersonelCode { get; set; }
		[Display(Name = "نام")]
		public string FirstName { get; set; }
		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; }
		[Display(Name = "اداره")]
		public int? DepartmentId { get; set; }
		public int? DepartmentTitle { get; set; }
		[Display(Name = "آدرس شبکه از")]
		[RegularExpression("^([0-9]{1,3}\\.){3}[0-9]{1,3}$", ErrorMessage = "فرمت {0} را درست وارد نمایید")]
		public string IpFrom { get; set; }
		[Display(Name = "آدرس شبکه تا")]
		[RegularExpression("^([0-9]{1,3}\\.){3}[0-9]{1,3}$", ErrorMessage = "فرمت {0} را درست وارد نمایید")]
		public string IpTo { get; set; }
		public SelectList AccessibilityLevelList { get; set; }
		public SelectList DepartmentList { get; set; }
	}
}
