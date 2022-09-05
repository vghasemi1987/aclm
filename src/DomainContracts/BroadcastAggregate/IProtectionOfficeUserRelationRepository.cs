using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;

namespace DomainContracts.BroadcastAggregate
{
	public interface IProtectionOfficeUserRelationRepository : IRepository<ProtectionOfficeUserRelation>, IAsyncRepository<ProtectionOfficeUserRelation>
	{

	}
}