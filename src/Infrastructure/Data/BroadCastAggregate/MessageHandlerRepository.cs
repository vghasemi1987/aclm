using DomainContracts.BroadcastAggregate;
using DomainEntities.BroadcastAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class MessageHandlerRepository : EfRepository<MessageHandler>, IMessageHandlerRepository
	{
		private readonly DbSet<MessageHandler> _dbSet;
		public MessageHandlerRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<MessageHandler>();
		}

		public async Task<MessageHandler> GetById(int? id)
		{
			var result = await _dbSet.FindAsync(id);
			return result;
		}

		public async Task<List<MessageHandler>> GetMessageAll()
		{
			var result = await _dbSet.ToListAsync();
			return result;
		}

		//public async Task Add(MessageHandler messageHandler)
		//{
		//	 await _dbSet.AddAsync(messageHandler);
		//}
	}
}
