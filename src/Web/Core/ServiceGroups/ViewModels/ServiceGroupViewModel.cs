using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.ServiceGroups.ViewModels
{
	public class ServiceGroupViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Name { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
	}
}
