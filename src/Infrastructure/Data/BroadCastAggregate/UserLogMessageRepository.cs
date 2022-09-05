using DomainContracts.BroadcastAggregate;
using DomainEntities.BroadcastAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ApplicationCommon;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class UserLogMessageRepository : EfRepository<UserLogMessage>, IUserLogMessageRepository
	{
		private readonly DbSet<UserLogMessage> _dbSet;
		public UserLogMessageRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{

		}
		//public async Task<IQueryable<UserLogMessage>> GetUserLogMessageAll()
		//{
		//    var model = (await DbContext.Set<UserLogMessage>().AsQueryable());
		//    return;
		//}
		IQueryable<UserLogMessage> IUserLogMessageRepository.GetUserLogMessageAll()
		{
			var model = DbContext.Set<UserLogMessage>().AsQueryable<UserLogMessage>();
			return model;
		}
		public DataSourceResult GetList(DataSourceRequest request)
		{
			return DbContext.Set<UserLogMessage>().Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted) /*&& q.DstUserID == dstUserID*/)
				 .Select(o => new
				 {
					 Id = o.Id,
					 Controller = o.Controller,
					 Action = o.Action,
					 DateActivity = o.DateActivity.ToPersianDateTimePcrTime(),
					 UserName = o.UserName,
					 Description = o.Description,
					 Ip = o.IP,
				 })
				 .AsNoTracking()
				 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
		}
	}
}
