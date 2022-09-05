using DomainEntities.AccessRangeAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.AccessRanges.ViewModels
{
	public class AccessRangeViewModel
	{
		[HiddenInput]
		public int Id { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "عنوان")]
		public string Title { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "آدرس شبکه از")]
		public string IpFrom { get; set; }
		[Required(ErrorMessage = "{0} را وارد نمایید")]
		[Display(Name = "آدرس شبکه تا")]
		public string IpTo { get; set; }
		public int? UserId { get; set; }
		public string UserName { get; set; }
		public List<AccessRangeDetail> AccessRangeDetails { get; set; } = new List<AccessRangeDetail>();
	}

	public class AccessRangeInfo
	{
		public int Id { get; set; }
		public string Title { get; set; }
	}
}
