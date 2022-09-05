using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.BroadcastAggregate
{
	public class GroupingOffice : Entity<int>
	{
		public string Title { get; set; }
		public List<GroupingOfficeMember> GroupingOfficeMembers { get; set; }
	}
	public class GroupingOfficeMember : Entity<int>
	{
		public int GroupingOfficeId { get; set; }
		public GroupingOffice GroupingOffice { get; set; }
		public int ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}