using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Web.Core.AclFilesUploads.ViewModels
{
	public class InvalidFileItemViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Display(Name = "عنوان نامعتبر")]
		public string InvalidItemTitle { get; set; }
		[Display(Name = "شماره سطر")]
		public int LineNumber { get; set; }
		public int AclFilesUploadId { get; set; }
		[Display(Name = "نام فایل")]
		public string AclFilesUploadTitle { get; set; }
	}
}
