using DomainEntities.BankBranchAggregate;
using DomainEntities.BroadcastAggregate;
using DomainEntities.DepartmentAggregate;
using DomainEntities.DutyPositionAggregate;
using DomainEntities.PositionAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DomainEntities.ApplicationUserAggregate
{
	public class ApplicationUser : IdentityUser<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PersonnelCode { get; set; }
		public string Picture { get; set; }
		public DateTime RegDateTime { get; private set; } = DateTime.Now;
		public int? DepartmentId { get; set; }
		public Department Department { get; set; }
		public int? PositionId { get; set; }
		public Position Position { get; set; }
		public int? BankBranchId { get; set; }
		public BankBranch BankBranch { get; set; }
		public int? DutyPositionId { get; set; }
		public DutyPosition DutyPosition { get; set; }
		public string Name => FirstName + " " + LastName;
		public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
		public ICollection<ApplicationUserAccessRageHeader> ApplicationUserAccessRageHeaders { get; set; }
		public bool ExpertPerformance { get; set; }
		public string ExpertPerformanceToken { get; set; }
		public bool Qualifications { get; set; }
		public string QualificationsToken { get; set; }
		//444
		public bool Qualifications_Second { get; set; }
		public string QualificationsToken_Second { get; set; }
		public List<ReferralBroadCast> referralBroadCast { get; set; }
		public ProtectionOfficeMember ProtectionOfficeMember { get; set; }
		public GroupingOfficeMember GroupingOfficeMember { get; set; }
	}
}
