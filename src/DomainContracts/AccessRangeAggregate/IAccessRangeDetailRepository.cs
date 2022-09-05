using DomainContracts.Commons;
using DomainEntities.AccessRangeAggregate;
using System.Collections.Generic;

namespace DomainContracts.AccessRangeAggregate
{
	public interface IAccessRangeDetailRepository : IRepository<AccessRangeDetail>, IAsyncRepository<AccessRangeDetail>
	{
		List<AccessRangeDetail> FindByHeaderId(List<int> headerIds);
	}
}
