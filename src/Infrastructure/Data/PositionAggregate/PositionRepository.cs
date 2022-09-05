using DomainContracts.PositionAggregate;
using DomainEntities.Commons;
using DomainEntities.PositionAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.PositionAggregate
{
	public class PositionRepository : EfRepository<Position>, IPositionRepository
	{
		private readonly DbSet<Position> _dbSet;
		public PositionRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<Position>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new PositionListDto
				 {
					 Id = o.Id,
					 Description = o.Description,
					 Title = o.Title
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<Position> FindByIdAsync(int id)
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
