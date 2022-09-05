using DomainEntities.ExcelFormatAggregate;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ExcelFormats.ViewModels
{
	public class ExcelFormatHeaderViewModel
	{
		public int Id { get; set; }
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "فایل پیوست"), Required(ErrorMessage = "{0} را وارد نمایید")]
		public IFormFile PostedFile { get; set; }
		[Display(Name = "نوع فایل"), Required(ErrorMessage = "{0} را وارد نمایید")]
		public FileFormat FileFormat { get; set; } = 0;
		[Display(Name = "جداکننده"),
			Required(ErrorMessage = "{0} را وارد نمایید"),
			/*StringLength(1, ErrorMessage = "حداکثر مقدار {0} {1} و حداقل {2} است", MinimumLength =1)*/]
		public string Separator { get; set; } /*= ";";*/
	}
}
