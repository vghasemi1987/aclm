using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Routers.ViewModels
{
	public class RouterViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "نام")]
		public string Name { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "شماره")]
		public string AccessNo { get; set; }
	}
}
