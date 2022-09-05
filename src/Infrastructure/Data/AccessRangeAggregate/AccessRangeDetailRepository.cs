using DomainContracts.AccessRangeAggregate;
using DomainEntities.AccessRangeAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.AccessRangeAggregate
{
	public class AccessRangeDetailRepository : EfRepository<AccessRangeDetail>, IAccessRangeDetailRepository
	{
		private readonly DbSet<AccessRangeDetail> _dbSet;
		public AccessRangeDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AccessRangeDetail>();
		}

		public List<AccessRangeDetail> FindByHeaderId(List<int> headerIds)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
										headerIds.Contains(q.AccessRangeHeaderId))
				 .Select(o => new AccessRangeDetail
				 {
					 Id = o.Id,
					 IpFrom = o.IpFrom,
					 IpTo = o.IpTo,
					 AccessRangeHeaderId = o.AccessRangeHeaderId
				 }).ToList();
		}
	}
}