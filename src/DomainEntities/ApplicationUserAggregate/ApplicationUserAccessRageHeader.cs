using DomainEntities.AccessRangeAggregate;
using DomainEntities.Commons;

namespace DomainEntities.ApplicationUserAggregate
{
	public class ApplicationUserAccessRageHeader : Entity<int>
	{
		public int ApplicationUserId { get; set; }
		public int AccessRangeHeaderId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual AccessRangeHeader AccessRangeHeader { get; set; }
	}
}
