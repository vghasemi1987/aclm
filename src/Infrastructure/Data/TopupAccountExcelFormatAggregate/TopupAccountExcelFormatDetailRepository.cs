using DomainContracts.TopupAccountExcelFormatAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupAccountExcelFormatAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TopupAccountExcelFormatAggregate
{
	public class TopupAccountExcelFormatDetailRepository : EfRepository<TopupAccountExcelFormatDetail>, ITopupAccountExcelFormatDetailRepository
	{
		private readonly DbSet<TopupAccountExcelFormatDetail> _dbSet;
		public TopupAccountExcelFormatDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupAccountExcelFormatDetail>();
		}

		public Task<List<TopupAccountExcelFormatDetail>> FindByHeaderIdAsync(int id)
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

		public void Delete(List<TopupAccountExcelFormatDetail> entityList)
		{
			_dbSet.RemoveRange(entityList);
		}

		public List<TopupAccountExcelFormatDetail> FindByHeaderId(int id)
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
