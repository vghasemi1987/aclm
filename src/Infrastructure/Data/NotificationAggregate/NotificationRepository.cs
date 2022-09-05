using Dapper;
using DomainContracts.NotificationAggregate;
using DomainEntities;
using DomainEntities.NotificationAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.NotificationAggregate
{
	public class NotificationRepository : EfRepository<NotificationItem>, INotificationRepository
	{

		private readonly SqlConnection _connection;
		private readonly DbSet<NotificationItem> _dbSet;

		public NotificationRepository(ServerAccessibilityMonitorContext dbContext, ConfigurationDto configuration) : base(dbContext)
		{
			_dbSet = dbContext.Set<NotificationItem>();
			_connection = new SqlConnection(configuration.ConnectionString);
		}

		public async Task<List<NotificationItem>> GetByUserIdNotificationsAsync(int userId, int count)
		{
			//return _dbSet.Where(o => o.ForUserId == userId)
			//	.OrderByDescending(o => o.Id)
			//	.Take(count)
			//	.Include(o => o.CreatedByUser)
			//	.AsNoTracking()
			//	.ToListAsync();

			return (await _connection.QueryAsync<NotificationItem>(@$"select
							top {count} 
							[Id], 
							[CategoryId],
							[CreatedByUserId], 
							[EntryDateTime], 
							[ForUserId], 
							[IsRead], 
							[IsSent], 
							[RecordStatus],
							[Text],
							[ToDoTaskId], 
							[Url]
							from NotificationItems
							where ForUserId = {userId}
							order by id ")).ToList();
		}

		public DataSourceResult GetByUserIdNotificationsAsync(DataSourceRequest request, int userId)
		{
			return _dbSet.Where(o => o.ForUserId == userId)
				.OrderByDescending(o => o.Id)
				.Include(o => o.CreatedByUser)
				.Include(o => o.Category)
				.AsNoTracking()
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
	}
}