using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.AccessTypes.ViewModels
{
	public class AccessTypeViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
	}
}
