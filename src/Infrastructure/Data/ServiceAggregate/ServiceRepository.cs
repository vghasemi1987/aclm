using DomainContracts.ServiceAggregate;
using DomainEntities.Commons;
using DomainEntities.ServiceAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ServiceAggregate
{
	public class ServiceGroupRepository : EfRepository<Service>, IServiceRepository
	{
		private readonly DbSet<Service> _dbSet;
		public ServiceGroupRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Service>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new ServiceListDto
				 {
					 Id = o.Id,
					 Name = o.Name,
					 Description = o.Description,
					 Port = o.Port,
					 ProtocolId = o.ProtocolId,
					 ProtocolTitle = o.Protocol.Name,
					 ServiceLevelId = o.ServiceLevelId,
					 ServiceLevelTitle = o.ServiceLevel.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<Service> FindByIdAsync(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
		public Task<List<DropDownDto>> GetDropDownList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Select(o => new DropDownDto
				{
					Id = o.Id,
					Title = o.Name
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public List<Service> GetList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.ToList();
		}
	}
}
