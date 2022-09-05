using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using System.Collections.Generic;

namespace DomainContracts.ApplicationUserAggregate
{
	public interface IApplicationUserAccessRageHeaderRepository : IRepository<ApplicationUserAccessRageHeader>, IAsyncRepository<ApplicationUserAccessRageHeader>
	{
		List<ApplicationUserAccessRageHeader> GetListByUserId(int userId);
	}
}
