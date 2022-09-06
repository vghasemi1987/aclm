using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.BroadcastAggregate
{
	public class ProtectionOffice : Entity<int>
	{
		public string Title { get; set; }
		public List<ProtectionOfficeMember> ProtectionOfficeMembers { get; set; }
		public List<ProtectionOfficeUserRelation> ProtectionOfficeUserRelations { get; set; }
	}
	public class ProtectionOfficeMember : Entity<int>
	{
		public int ProtectionOfficeId { get; set; }
		public ProtectionOffice ProtectionOffice { get; set; }
		public int ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}