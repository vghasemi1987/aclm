using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Services.ViewModels
{
	public class ServiceViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Name { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پورت")]
		public int? Port { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پروتکل")]
		public int? ProtocolId { get; set; }
		public string ProtocolTitle { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "سطح حساسیت")]
		public int? ServiceLevelId { get; set; }
		public string ServiceLevelTitle { get; set; }
		public SelectList ProtocolSelectList { get; set; }
		public SelectList ServiceLevelSelectList { get; set; }
	}
}
