using DomainContracts.ApplicationUserAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.ApplicationUserAggregate
{
	public class ApplicationUserAccessRageHeaderRepository : EfRepository<ApplicationUserAccessRageHeader>, IApplicationUserAccessRageHeaderRepository
	{
		private DbSet<ApplicationUserAccessRageHeader> _dbSet { get; }

		public ApplicationUserAccessRageHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ApplicationUserAccessRageHeader>();
		}
		//دسترسی کاربران به آی پی
		public List<ApplicationUserAccessRageHeader> GetListByUserId(int userId)
		{
			var result = _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
							   q.ApplicationUserId == userId).
								ToList();
			return result;

		}
	}
}