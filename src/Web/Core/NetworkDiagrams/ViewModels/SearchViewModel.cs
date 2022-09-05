using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.NetworkDiagram.ViewModels
{
	public class SearchViewModel
	{
		[Display(Name = "نام موجودیت")]
		public int? SystemId { get; set; }
		[Display(Name = "پورت")]
		public int? ServiceId { get; set; }
		[Display(Name = "آدرس شامل")]
		public string Address { get; set; }
		[Display(Name = "روتر")]
		public int? RouterId { get; set; }
		[Display(Name = "سطح")]
		public int? Level { get; set; }
		[Display(Name = "تعداد دسترسی")]
		public int AccessCount { get; set; }
		[Display(Name = "آدرس مبدا")]
		public string SourceIp { get; set; }
		[Display(Name = "آدرس مقصد")]
		public string DestinationIp { get; set; }
		[Display(Name = "کد پرسنلی")]
		public int PersonelCode { get; set; }
		[Display(Name = "تاریخ بازیابی")]
		[RegularExpression(@"\d{4,4}/\d{2,2}/\d{2,2} \d{2,2}:\d{2,2}:\d{2,2}", ErrorMessage = "{0} وارد شده نامعتبر است.")]
		public string RecoveryDate { get; set; }

		public SelectList SystemList { get; set; }
		public SelectList ServiceList { get; set; }
		public SelectList RouterList { get; set; }
		[Display(Name = "ضریب اهمیت")]
		public int? ImportanceFactor { get; set; }
	}
}
