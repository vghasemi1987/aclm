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
	public class TopupChannelExcelFormatHeaderRepository : EfRepository<TopupChannelExcelFormatHeader>, ITopupChannelExcelFormatHeaderRepository
	{
		private readonly DbSet<TopupChannelExcelFormatHeader> _dbSet;

		public TopupChannelExcelFormatHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupChannelExcelFormatHeader>();
		}
		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.AsNoTracking().ToDataSourceResult(request);
		}
		public async Task<TopupChannelExcelFormatHeader> FindByIdAsync(int id)
		{
			return await _dbSet.Where(p => p.Id == id).FirstOrDefaultAsync();
		}
		public TopupChannelExcelFormatHeader FindById(int id)
		{
			return _dbSet.Where(p => p.Id == id).FirstOrDefault();
		}
		public async Task<List<DropDownDto>> GetDropDownList()
		{
			return await _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
			.Select(q => new DropDownDto
			{
				Id = q.Id,
				Title = q.Title
			}).ToListAsync();
		}
		public void Delete(List<int> idList)
		{
			foreach (var id in idList)
			{
				var entity = FindById(id);
				Delete(entity);
			}
		}


	}
}
