using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IProtectionOfficeMemberRepository : IRepository<ProtectionOfficeMember>, IAsyncRepository<ProtectionOfficeMember>
	{
		Task<List<ProtectionOfficeMember>> GetById(int? id);
		Task<List<ProtectionOfficeMember>> GetByProtectionOfficeId(int? id);
		Task<List<ProtectionOfficeMember>> GetByProtectionOfficeUserId(List<int> userIdList);
	}
}