using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.AuthorityAggregate
{
	public class Authority : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public ICollection<ApplicationRoleAuthority> ApplicationRoleAuthorities { get; set; }
	}
}