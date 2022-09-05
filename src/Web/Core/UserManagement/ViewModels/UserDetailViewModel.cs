using System;

namespace Web.Core.UserManagement.ViewModels
{
	public class UserDetailViewModel
	{
		public int Id { get; set; }
		public string Picture { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Name { get; set; }
		public DateTime RegDateTime { get; set; }
		public string Roles { get; set; }
		public bool LockoutState { get; set; }
		public string LockoutStateName
		{
			get
			{
				if (LockoutState) return "غیرفعال";
				else return "فعال";
			}
		}
		public string DepartmentName { get; set; }
		public string PositionName { get; set; }
		public string BankBranchName { get; set; }
		public int DutyPositionId { get; set; }
		public string DutyPositionTitle { get; set; }
	}
}
