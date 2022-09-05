using ApplicationCommon;
using DomainContracts.TopupAccountTransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TopupAccountTransaction;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TopupAccountTransactionAggregate
{
	public class TopupAccountTransactionHeaderRepository : EfRepository<TopupAccountTransactionHeader>, ITopupAccountTransactionHeaderRepository
	{
		private readonly DbSet<TopupAccountTransactionHeader> _dbSet;
		public TopupAccountTransactionHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TopupAccountTransactionHeader>();
		}

		public Task<TopupAccountTransactionHeader> FindByIdAsync(int id)
		{
			return _dbSet.FirstOrDefaultAsync(q => q.Id == id);
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
						.Select(q => new TopupAccountTransactionHeaderListDto
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
