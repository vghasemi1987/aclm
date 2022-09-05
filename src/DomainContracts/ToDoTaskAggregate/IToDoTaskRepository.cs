using DomainContracts.Commons;
using DomainEntities.ToDoTaskAggregate;
using KendoHelper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainContracts.ToDoTaskAggregate
{
	public interface IToDoTaskRepository : IRepository<ToDoTask>, IAsyncRepository<ToDoTask>
	{
		Task<List<ToDoTask>> ListByFollowIdAsync(int followId);
		DataSourceResult GetList(DataSourceRequest request);
		Task<ToDoTask> FindByIdAsync(int id);
		Task Delete(IEnumerable<int> ids);
		Task<List<ToDoTaskNotificationListDto>> ListByUserIdForNotification(int userId);
		Task<ToDoTask> GetByIdWihAttachmentDetail(int id);
		Task<List<ToDoTask>> ListByContactIdAsync(int contactId);
		Task<List<ToDoTask>> ListByContractIdAsync(int contractId);
		DataSourceResult GetByQuery(DataSourceRequest request, string query);
		Task<List<ToDoTask>> GetByCriteria(Expression<Func<ToDoTask, bool>> criteria);
	}
}