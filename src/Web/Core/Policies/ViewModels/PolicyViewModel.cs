using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Policies.ViewModels
{
	public class PolicyViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "پروتکل")]
		public int? ProtocolId { get; set; }
		public SelectList ProtocolSelectList { get; set; }
	}
}
