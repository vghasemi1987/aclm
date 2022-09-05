using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using System.Collections.Generic;

namespace DomainContracts.AuthorityAggregate
{
	public interface IApplicationRoleAuthorityRepository : IRepository<ApplicationRoleAuthority>, IAsyncRepository<ApplicationRoleAuthority>
	{
		List<ApplicationRoleAuthority> GetListByRoleId(int roleId);
	}
}
