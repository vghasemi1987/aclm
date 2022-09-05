using DomainEntities.Commons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Core.Authorities.ViewModels
{
	public class AuthorityRoleViewModel
	{
		public int RoleId { get; set; }
		[Display(Name = "عنوان")]
		public string RoleTitle { get; set; }
		public List<DropDownDto> AuthorityInfos { get; set; }
		public List<string> SelectList { get; set; } = new List<string>();
	}
}
