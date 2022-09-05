using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;

namespace DomainEntities.BroadcastAggregate
{
	public class ProtectionOfficeUserRelation : Entity<int>
	{
		public int ProtectionOfficeId { get; set; }
		public ProtectionOffice ProtectionOffice { get; set; }
		public int ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}