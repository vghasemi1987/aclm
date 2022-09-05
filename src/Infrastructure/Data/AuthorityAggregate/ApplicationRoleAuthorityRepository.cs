using DomainContracts.AuthorityAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.AuthorityAggregate
{
	public class ApplicationRoleAuthorityRepository : EfRepository<ApplicationRoleAuthority>, IApplicationRoleAuthorityRepository
	{
		private readonly DbSet<ApplicationRoleAuthority> _dbSet;
		public ApplicationRoleAuthorityRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ApplicationRoleAuthority>();
		}
		public List<ApplicationRoleAuthority> GetListByRoleId(int roleId)
		{
			return _dbSet.Where(p => p.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) &&
			p.ApplicationRoleId == roleId)
				.ToList();
		}
	}
}
