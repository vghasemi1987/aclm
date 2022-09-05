using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Web.Core.TopupChannelTransactions.ViewModels
{
	public class TopupChannelTransactionViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Display(Name = "فایل")]
		public string PostedFile { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نام فایل")]
		public string FileName { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نوع فرمت")]
		public int ExcelFormatHeaderId { get; set; }
		[Display(Name = "تاریخ ثبت فایل")]
		public string UploadDate { get; set; }
		[Display(Name = "نوع فایل")]
		public short FileKindId { get; set; } = 0;
		public SelectList ExcelFormatHeaderList { get; set; }

	}
}
