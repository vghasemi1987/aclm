using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Core.AccessRanges.ViewModels;

namespace Web.Core.UserManagement.ViewModels
{
	public class UserAccessRangeViewModel
	{
		public int UserId { get; set; }
		[Display(Name = "عنوان")]
		public string UserName { get; set; }
		public List<AccessRangeInfo> AccessRangeInfos { get; set; }
		public List<string> SelectList { get; set; } = new List<string>();
	}
}
