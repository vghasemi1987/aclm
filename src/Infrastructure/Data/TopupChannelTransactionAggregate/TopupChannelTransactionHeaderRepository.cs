using ApplicationCommon;
using DomainContracts.TopupChannelTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupChannelTransaction;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TopupChannelTransactionAggregate
{
	public class TopupChannelTransactionHeaderRepository : EfRepository<TopupChannelTransactionHeader>, ITopupChannelTransactionHeaderRepository
	{
		private readonly DbSet<TopupChannelTransactionHeader> _dbSet;
		public TopupChannelTransactionHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupChannelTransactionHeader>();
		}

		public Task<TopupChannelTransactionHeader> FindByIdAsync(int id)
		{
			return _dbSet.FirstOrDefaultAsync(q => q.Id == id);
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
						.Select(q => new TopupChannelTransactionHeaderListDto
						{
							Id = q.Id,
							RecordStatus = q.RecordStatus,
							UserId = q.UserId,
							Title = q.Title,
							Description = q.Description,
							UserName = q.User.UserName,
							UploadDate = q.UploadDate.ToPersianDateTime("yyyy/MM/dd")
						})
						.AsNoTracking().ToDataSourceResult(request);
		}
	}
}
