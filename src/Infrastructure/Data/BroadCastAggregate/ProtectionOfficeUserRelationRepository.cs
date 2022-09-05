using DomainContracts.BroadcastAggregate;
using DomainEntities.BroadcastAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ProtectionOfficeUserRelationRepository : EfRepository<ProtectionOfficeUserRelation>, IProtectionOfficeUserRelationRepository
	{
		private readonly DbSet<ProtectionOfficeUserRelation> _dbSet;
		public ProtectionOfficeUserRelationRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<ProtectionOfficeUserRelation>();
		}
	}
}
