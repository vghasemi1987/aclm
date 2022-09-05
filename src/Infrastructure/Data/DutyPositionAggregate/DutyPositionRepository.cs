using DomainContracts.DutyPositionAggregate;
using DomainEntities.Commons;
using DomainEntities.DutyPositionAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.DutyPositionAggregate
{
	public class DutyPositionRepository : EfRepository<DutyPosition>, IDutyPositionRepository
	{
		private readonly DbSet<DutyPosition> _dbSet;
		public DutyPositionRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<DutyPosition>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new DutyPositionListDto
				 {
					 Id = o.Id,
					 Title = o.Title,
					 Code = o.Code
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<DutyPosition> FindByIdAsync(int id)
		{
			return _dbSet
				.Where(q => q.Id == id)
				.FirstOrDefaultAsync();
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

		public List<DutyPosition> GetList()
		{
			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.ToList();
		}
	}
}
