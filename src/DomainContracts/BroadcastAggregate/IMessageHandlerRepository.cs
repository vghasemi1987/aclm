using System.Threading.Tasks;
using System.Collections.Generic;
using DomainEntities.BroadcastAggregate;
using DomainContracts.Commons;

namespace DomainContracts.BroadcastAggregate
{
	public interface IMessageHandlerRepository : IRepository<MessageHandler>, IAsyncRepository<MessageHandler>
	{
		Task<MessageHandler> GetById(int? id);
		Task<List<MessageHandler>> GetMessageAll();
	}
}
