using DomainContracts.ServiceGroupAggregate;
using DomainEntities.Commons;
using DomainEntities.ServiceGroupAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ServiceGroupAggregate
{
	public class ServiceGroupRepository : EfRepository<ServiceGroup>, IServiceGroupRepository
	{
		private readonly DbSet<ServiceGroup> _dbSet;
		public ServiceGroupRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ServiceGroup>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new ServiceGroupListDto
				 {
					 Id = o.Id,
					 Name = o.Name,
					 Description = o.Description,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<ServiceGroup> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
	}
}
