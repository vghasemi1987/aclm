using DomainContracts.ProtocolAggregate;
using DomainEntities.Commons;
using DomainEntities.ProtocolAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ProtocolAggregate
{
	public class ProtocolRepository : EfRepository<Protocol>, IProtocolRepository
	{
		private readonly DbSet<Protocol> _dbSet;
		public ProtocolRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Protocol>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new ProtocolListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 Name = o.Name
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<Protocol> FindByIdAsync(int id)
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

		public List<Protocol> GetList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.ToList();
		}
	}
}
