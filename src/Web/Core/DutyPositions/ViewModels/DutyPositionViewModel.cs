using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.DutyPositions.ViewModels
{
	public class DutyPositionViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "کد")]
		public int Code { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }
	}
}
