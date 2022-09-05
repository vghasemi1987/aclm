using DomainContracts.AccessRangeAggregate;
using DomainEntities.AccessRangeAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.AccessRangeAggregate
{
	public class AccessRangeHeaderRepository : EfRepository<AccessRangeHeader>, IAccessRangeHeaderRepository
	{
		private readonly DbSet<AccessRangeHeader> _dbSet;
		public AccessRangeHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AccessRangeHeader>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new AccessRangeHeaderListDto
				 {
					 Id = o.Id,
					 Title = o.Title,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<AccessRangeHeader> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
		public List<DropDownDto> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new DropDownDto
				{
					Id = o.Id,
					Title = o.Title
				})
				.AsNoTracking()
				.ToList();
		}
	}
}
