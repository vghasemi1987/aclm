using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.AccessRangeAggregate
{
	public class AccessRangeHeader : Entity<int>
	{
		public string Title { get; set; }
		public ICollection<ApplicationUserAccessRageHeader> ApplicationUserAccessRageHeaders { get; set; }
	}
}