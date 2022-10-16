using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IGroupingOfficeRepository : IRepository<GroupingOffice>, IAsyncRepository<GroupingOffice>
	{
		Task<GroupingOffice> GetById( int? id );
		Task<List<GroupingOffice>> GetGroupingOfficeAll();
		Task<List<GroupingOfficeMember>> GetByGroupOfficeId( int? id );
		Task<List<ApplicationUser>> GetUserBySubject( List<ApplicationUser> destinationUser , string title );
	}
	public interface IGroupingOfficeMemberRepository : IRepository<GroupingOfficeMember>, IAsyncRepository<GroupingOfficeMember>
	{
		Task<List<GroupingOfficeMember>> GetByGroupingOfficeId( int? id );
		Task DeleteByGroupingOfficeMemberListAsync( int id );
		Task<GroupingOfficeMember> GetByUserIdAndGroupingOfficeId( int userId , int groupId );
		Task<GroupingOfficeMember> GetByIdentityId( int id );
	}

}
