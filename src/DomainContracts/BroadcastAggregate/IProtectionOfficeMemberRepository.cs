﻿using DomainContracts.Commons;
using DomainEntities.BroadcastAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.BroadcastAggregate
{
	public interface IProtectionOfficeMemberRepository : IRepository<ProtectionOfficeMember>, IAsyncRepository<ProtectionOfficeMember>
	{
		Task<List<ProtectionOfficeMember>> GetById( int? id );
		Task<List<ProtectionOfficeMember>> GetByProtectionOfficeId( int? id );
		Task<List<ProtectionOfficeMember>> GetByProtectionOfficeUserId( List<int> userIdList );
		Task DeleteByprotectionMemberIdListAsync( int id );
		Task<ProtectionOfficeMember> GetByUserIdAndProtectionOfficeId( int userId , int groupId );
		Task<ProtectionOfficeMember> GetByIdentityId( int id );
	}
}