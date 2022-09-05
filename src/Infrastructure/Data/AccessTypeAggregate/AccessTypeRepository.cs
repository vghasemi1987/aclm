using DomainContracts.AccessTypeAggregate;
using DomainEntities.AccessTypeAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure.Data.AccessTypeAggregate
{
	public class AccessTypeRepository : EfRepository<AccessType>, IAccessTypeRepository
	{
		private readonly DbSet<AccessType> _dbSet;

		public AccessTypeRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<AccessType>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new AccessTypeListDto
				{
					Id = o.Id,
					Title = o.Title,
					Description = o.Description
				})
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<AccessType> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
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
