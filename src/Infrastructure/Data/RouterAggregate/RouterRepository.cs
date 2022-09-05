using DomainContracts.RouterAggregate;
using DomainEntities.Commons;
using DomainEntities.RouterAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.RouterAggregate
{
	public class ServiceRepository : EfRepository<Router>, IRouterRepository
	{
		private readonly DbSet<Router> _dbSet;
		public ServiceRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Router>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new RouterListDto
				 {
					 Id = o.Id,
					 Name = o.Name,
					 AccessNo = o.AccessNo
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<Router> FindByIdAsync(int id)
		{
			return _dbSet.Where(q => q.Id == id).FirstOrDefaultAsync();
		}

		public Task<List<DropDownDto>> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
			   .Select(o => new DropDownDto
			   {
				   Id = o.Id,
				   Title = o.Name
			   }).AsNoTracking().ToListAsync();
		}
	}
}
