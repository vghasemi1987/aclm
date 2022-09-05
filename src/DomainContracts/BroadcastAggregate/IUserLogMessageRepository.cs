using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using KendoHelper;
using System.Linq;

namespace DomainContracts.BroadcastAggregate
{
	public interface IUserLogMessageRepository : IRepository<UserLogMessage>, IAsyncRepository<UserLogMessage>
	{
		IQueryable<UserLogMessage> GetUserLogMessageAll();
		DataSourceResult GetList(DataSourceRequest request);
	}
}
