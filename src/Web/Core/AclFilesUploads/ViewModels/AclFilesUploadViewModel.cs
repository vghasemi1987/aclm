using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Web.Core.AclFilesUploads.ViewModels
{
	public class AclFilesUploadViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Display(Name = "فایل")]
		public IFormFile Files { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نام فایل")]
		public string FileName { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "روتر")]
		public int? RouterId { get; set; }
		public SelectList RouterSelectList { get; set; }
	}
}
