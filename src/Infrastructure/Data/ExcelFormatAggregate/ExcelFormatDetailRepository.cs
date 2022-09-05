using DomainContracts.ExcelFormatAggregate;
using DomainEntities.Commons;
using DomainEntities.ExcelFormatAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.ExcelFormatAggregate
{
	public class ExcelFormatDetailRepository : EfRepository<ExcelFormatDetail>, IExcelFormatDetailRepository
	{
		private readonly DbSet<ExcelFormatDetail> _dbSet;
		public ExcelFormatDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ExcelFormatDetail>();
		}

		public Task<List<ExcelFormatDetail>> FindByHeaderIdAsync(int id)
		{
			return _dbSet.Where(q => q.HeaderId == id)
				.ToListAsync();
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.AsNoTracking()
				.ToDataSourceResult(request);
		}

		public void Delete(List<ExcelFormatDetail> entityList)
		{
			_dbSet.RemoveRange(entityList);
		}

		public List<ExcelFormatDetail> FindByHeaderId(int id)
		{
			return _dbSet.Where(q => q.HeaderId == id).ToList();
		}

		public void Delete(List<int> idLists)
		{
			foreach (var id in idLists)
			{
				var detailList = FindByHeaderId(id);
				Delete(detailList);
			}
		}
	}
}
