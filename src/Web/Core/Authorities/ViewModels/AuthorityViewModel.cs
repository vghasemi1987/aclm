using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Authorities.ViewModels
{
	public class AuthorityViewModel
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
