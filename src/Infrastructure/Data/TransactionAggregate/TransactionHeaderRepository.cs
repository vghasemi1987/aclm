using ApplicationCommon;
using DomainContracts.TransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.Transaction;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TransactionAggregate
{
	public class TransactionHeaderRepository : EfRepository<TransactionHeader>, ITransactionHeaderRepository
	{
		private readonly DbSet<TransactionHeader> _dbSet;
		public TransactionHeaderRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<TransactionHeader>();
		}

		public async Task<TransactionHeader> FindByIdAsync(int id)
		{
			var result = await _dbSet.FindAsync(id);
			return result;
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
						.Select(q => new TransactionHeaderListDto
						{
							Id = q.Id,
							RecordStatus = q.RecordStatus,
							UserId = q.UserId,
							Title = q.Title,
							Description = q.Description,
							UserName = q.User.UserName,
							UploadDate = q.UploadDate.ToPersianDateTime("yyyy/MM/dd"),
							CountTransactionDetails = q.Details.Count(),
						})
						.AsNoTracking().ToDataSourceResult(request);
		}
	}
}
