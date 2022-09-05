using ApplicationCommon;
using Dapper;
using DomainContracts.ToDoTaskAggregate;
using DomainEntities;
using DomainEntities.ToDoTaskAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.ToDoTaskAggregate
{
	public class ToDoTaskRepository : EfRepository<ToDoTask>, IToDoTaskRepository
	{
		private readonly SqlConnection _connection;
		private DbSet<ToDoTask> _dbSet { get; }

		public ToDoTaskRepository(ServerAccessibilityMonitorContext dbContext, ConfigurationDto configuration) : base(dbContext)
		{
			_dbSet = dbContext.Set<ToDoTask>();
			_connection = new SqlConnection(configuration.ConnectionString);
		}

		public Task<ToDoTask> FindByIdAsync(int id)
		{
			return _dbSet.Where(o => o.Id == id)
				.Include(o => o.Priority)
				.Include(o => o.State)
				.Include(o => o.CreatorUser)
				.Include(o => o.AssignedToUser)
				.FirstOrDefaultAsync();
		}
		public Task<ToDoTask> GetByIdWihAttachmentDetail(int id)
		{
			return _dbSet.Where(o => o.Id == id)
				.FirstOrDefaultAsync();
		}
		public Task<List<ToDoTask>> ListByFollowIdAsync(int followId)
		{
			return _dbSet.OrderByDescending(o => o.Id)
				.Include(o => o.Priority)
				.Include(o => o.State)
				.Include(o => o.AssignedToUser)
				.AsNoTracking()
				.ToListAsync();
		}
		public Task<List<ToDoTask>> ListByContractIdAsync(int contractId)
		{
			return _dbSet
				.OrderByDescending(o => o.Id)
				.Include(o => o.Priority)
				.Include(o => o.State)
				.Include(o => o.AssignedToUser)
				.AsNoTracking()
				.ToListAsync();
		}
		public Task<List<ToDoTask>> ListByContactIdAsync(int contactId)
		{
			return _dbSet
				.OrderByDescending(o => o.Id)
				.Include(o => o.Priority)
				.Include(o => o.State)
				.Include(o => o.AssignedToUser)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<ToDoTaskNotificationListDto>> ListByUserIdForNotification(int userId)
		{
			//return await _dbSet.Where(o => o.AssignedToUserId == userId && o.StateId != 3)
			//	.Select(o => new ToDoTaskNotificationListDto
			//	{
			//		Title = o.Title,
			//		DueDateTime = o.DueDateTime.HasValue ? o.DueDateTime.Value.ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//		Id = o.Id,
			//		Status = o.State.Title
			//	})
			//	.AsNoTracking()
			//	.ToListAsync();

			return (await _connection.QueryAsync<ToDoTaskNotificationListDto>(@$"select 
					t1.Title,
					format(t1.DueDateTime,'yyyy/MM/dd','fa') as DueDateTime,
					t1.Id,
					t2.Title as [Status]
					from ToDoTasks t1
					left join ToDoTask_States t2 
					on t1.StateId = t2.Id
					where t1.AssignedToUserId = {userId} and t1.StateId <> 3")).ToList();

		}

		public async Task Delete(IEnumerable<int> ids)
		{
			var selectedRecords = await _dbSet.Join(ids, r => r.Id, at => at, (r, at) => r)
				.ToListAsync();
			foreach (var record in selectedRecords)
			{
				_dbSet.Remove(record);
			}
		}

		public DataSourceResult GetList(DataSourceRequest request)
		{
			return _dbSet
				.Select(o => new ToDoTaskListDto
				{
					Id = o.Id,
					Title = o.Title,
					CreatorUser = o.CreatorUser.Name,
					State = o.State.Title,
					Priority = o.Priority.Title,
					DueDateTime = o.DueDateTime,
					EntryDateTime = o.EntryDateTime
				})
				.AsNoTracking()
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}

		public DataSourceResult GetByQuery(DataSourceRequest request, string query)
		{
			return _dbSet.FromSqlRaw(query)
				.Select(o => new ToDoTaskListDto
				{
					Id = o.Id,
					Title = o.Title,
					CreatorUser = o.CreatorUser.Name,
					State = o.State.Title,
					Priority = o.Priority.Title,
					DueDateTime = o.DueDateTime,
					EntryDateTime = o.EntryDateTime
				})
				.AsNoTracking()
				.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
		public Task<List<ToDoTask>> GetByCriteria(Expression<Func<ToDoTask, bool>> criteria)
		{
			return _dbSet.Where(criteria)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}