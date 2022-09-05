using DomainEntities.AuthorityAggregate;
using DomainEntities.Commons;

namespace DomainEntities.ApplicationUserAggregate
{
	public class ApplicationRoleAuthority : Entity<int>
	{
		public int ApplicationRoleId { get; set; }
		public int AuthorityId { get; set; }
		public Authority Authority { get; set; }
		public ApplicationRole ApplicationRole { get; set; }
	}
}
