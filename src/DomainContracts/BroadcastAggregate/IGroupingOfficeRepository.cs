using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IGroupingOfficeRepository : IRepository<GroupingOffice>, IAsyncRepository<GroupingOffice>
	{
		Task<GroupingOffice> GetById(int? id);
		Task<List<GroupingOffice>> GetGroupingOfficeAll();
		Task<List<GroupingOfficeMember>> GetByGroupOfficeId(int? id);
	}
	public interface IGroupingOfficeMemberRepository : IRepository<GroupingOfficeMember>, IAsyncRepository<GroupingOfficeMember>
	{
		Task<List<GroupingOfficeMember>> GetById(int? id);
		Task DeleteByGroupingOfficeMemberListAsync(int id);
	}
	
}
