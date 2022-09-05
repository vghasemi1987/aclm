using DomainContracts.AccessibilityLevelAggregate;
using DomainEntities.AccessibilityLevelAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AccessibilityLevelAggregate
{
	public class AccessibilityLevelRepository : EfRepository<AccessibilityLevel>, IAccessibilityLevelRepository
	{
		private readonly DbSet<AccessibilityLevel> _dbSet;
		public AccessibilityLevelRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AccessibilityLevel>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AccessibilityLevelListDto
				 {
					 Id = o.Id,
					 Title = o.Title,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<List<DropDownDto>> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new DropDownDto
				{
					Id = o.Id,
					Title = o.Title
				})
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
