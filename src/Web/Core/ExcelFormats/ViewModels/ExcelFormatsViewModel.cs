using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ExcelFormats.ViewModels
{
	public class ExcelFormatViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }

		[Display(Name = "فرمت فایل اکسل")]
		public string Files { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		public int HeaderId { get; set; }
		[Display(Name = "شماره کارت مبدا")]
		public string SourcePan { get; set; }
		[Display(Name = "شماره کارت مقصد")]
		public string TargetPan { get; set; }
		[Display(Name = "تاریخ")]
		public string TransactionDate { get; set; }
		[Display(Name = "زمان")]
		public string TransactionTime { get; set; }
		[Display(Name = "تلفن")]
		public string Tel { get; set; }
		[Display(Name = "مبلغ")]
		public string Amount { get; set; }
		[Display(Name = "آدرس شبکه")]
		public string IpAddress { get; set; }
		public string LogDate { get; set; }
		[Display(Name = "وضعیت")]
		public string Status { get; set; }
		[Display(Name = "شماره مرجع")]
		public string RefNumber { get; set; }
		public string UserAgent { get; set; }
		public string Application { get; set; }
		public SelectList PropList { get; set; }

	}
}
