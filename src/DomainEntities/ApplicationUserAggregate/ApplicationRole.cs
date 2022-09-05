using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

namespace DomainEntities.ApplicationUserAggregate
{
	public sealed class ApplicationRole : IdentityRole<int>
	{
		public ApplicationRole()
		{

		}
		public ApplicationRole(string roleName) : this()
		{
			Name = roleName;
		}

		public string PanelMenu { get; set; }
		public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
		public ICollection<ApplicationRoleAuthority> ApplicationRoleAuthorities { get; set; }
	}

	public enum ApplicationRolesEnum
	{
		[Description("developer")]
		Developer = 1,
		[Description("admin")]
		Admin = 2,
		[Description("اداره حفاظت")]
		ProtectionOffice = 3,
		[Description("اداره پشتیبانی")]
		SupportOffice = 4,
		[Description("امور شعب")]
		BranchHead = 5,
		[Description("کاربر گزارش")]
		ReportUser = 6
	}
}
