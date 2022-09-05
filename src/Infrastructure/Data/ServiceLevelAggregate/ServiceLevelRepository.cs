using DomainContracts.ServiceLevelAggregate;
using DomainEntities.Commons;
using DomainEntities.ServiceLevelAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ServiceLevelAggregate
{
	public class ServiceLevelRepository : EfRepository<ServiceLevel>, IServiceLevelRepository
	{
		private readonly DbSet<ServiceLevel> _dbSet;
		public ServiceLevelRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ServiceLevel>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new ServiceLevelListDto
				 {
					 Id = o.Id,
					 Title = o.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<ServiceLevel> FindByIdAsync(int id)
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
