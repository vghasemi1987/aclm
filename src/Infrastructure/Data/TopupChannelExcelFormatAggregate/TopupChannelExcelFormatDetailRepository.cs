using DomainContracts.TopupChannelExcelFormatAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupChannelExcelFormatAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TopupChannelExcelFormatAggregate
{
	public class TopupChannelExcelFormatDetailRepository : EfRepository<TopupChannelExcelFormatDetail>, ITopupChannelExcelFormatDetailRepository
	{
		private readonly DbSet<TopupChannelExcelFormatDetail> _dbSet;
		public TopupChannelExcelFormatDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupChannelExcelFormatDetail>();
		}

		public Task<List<TopupChannelExcelFormatDetail>> FindByHeaderIdAsync(int id)
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

		public void Delete(List<TopupChannelExcelFormatDetail> entityList)
		{
			_dbSet.RemoveRange(entityList);
		}

		public List<TopupChannelExcelFormatDetail> FindByHeaderId(int id)
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
