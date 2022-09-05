using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.TopupChannelExcelFormats.ViewModels
{
	public class TopupChannelExcelFormatHeaderViewModel
	{
		public int Id { get; set; }
		[Display(Name = "عنوان")]
		public string Title { get; set; }

		[Display(Name = "فایل پیوست")]
		public IFormFile PostedFile { get; set; }

		[Display(Name = "نوع فایل")]
		public short FileKindId { get; set; } = 0;
	}
}
