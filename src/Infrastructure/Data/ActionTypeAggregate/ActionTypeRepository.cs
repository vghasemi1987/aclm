using DomainContracts.ActionTypeAggregate;
using DomainEntities.ActionTypeAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ActionTypeAggregate
{
	public class ActionTypeRepository : EfRepository<ActionType>, IActionTypeRepository
	{
		private readonly DbSet<ActionType> _dbSet;
		public ActionTypeRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ActionType>();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				 .Select(o => new ActionTypeListDto
				 {
					 Id = o.Id,
					 Title = o.Title,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public Task<ActionType> FindByIdAsync(int id)
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

		public List<ActionType> GetList()
		{
			return _dbSet
				.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.ToList();
		}
	}
}
