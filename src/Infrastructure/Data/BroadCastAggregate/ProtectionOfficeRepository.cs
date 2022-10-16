using DomainContracts.BroadcastAggregate;
using DomainEntities.BroadcastAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class ProtectionOfficeRepository : EfRepository<ProtectionOffice>, IProtectionOfficeRepository
	{
		private readonly DbSet<ProtectionOffice> _dbSet;
		public ProtectionOfficeRepository( ServerAccessibilityMonitorContext dbContext ) : base( dbContext )
		{
			_dbSet = dbContext.Set<ProtectionOffice>();
		}

		public async Task<ProtectionOffice> GetById( int? id )
		{
			return await DbContext.Set<ProtectionOffice>().Include( c => c.ProtectionOfficeUserRelations ).AsNoTracking().SingleOrDefaultAsync( c => c.Id == id );
		}


		/// <summary>
		/// لسیت موضوعات
		/// </summary>
		/// <returns></returns>
		public async Task<List<ProtectionOffice>> GetProtectionOfficeAll()
		{
			List<ProtectionOffice> result =
				await DbContext.Set<ProtectionOffice>().Include( c => c.ProtectionOfficeMembers ).Include( c => c.ProtectionOfficeUserRelations ).AsNoTracking().ToListAsync();
			return result;
		}
	}
	public class ProtectionOfficeMemberRepository : EfRepository<ProtectionOfficeMember>, IProtectionOfficeMemberRepository
	{
		private readonly DbSet<ProtectionOfficeMember> _dbSet;
		public ProtectionOfficeMemberRepository( ServerAccessibilityMonitorContext dbContext ) : base( dbContext )
		{
			_dbSet = dbContext.Set<ProtectionOfficeMember>();
		}

		public async Task DeleteByprotectionMemberIdListAsync( int id )
		{
			List<ProtectionOfficeMember> result =
				await ( _dbSet.Where( w => w.ProtectionOfficeId == id ) ).ToListAsync();
			foreach ( ProtectionOfficeMember item in result )
			{
				_dbSet.Remove( _dbSet.Find( item.Id ) );
			}
		}
		public async Task<ProtectionOfficeMember> GetByUserIdAndProtectionOfficeId( int userId , int groupId )
		{
			var model =
				await DbContext.Set<ProtectionOfficeMember>()
				.Where( w => w.ApplicationUserId == userId && w.ProtectionOfficeId == groupId )
				.Include( c => c.ApplicationUser ).AsNoTracking()
				.FirstOrDefaultAsync();
			return model;
		}
		public async Task<List<ProtectionOfficeMember>> GetById( int? id )
		{
			var model = await DbContext.Set<ProtectionOfficeMember>().Where( w => w.ProtectionOfficeId == id )
				.Include( c => c.ApplicationUser ).AsNoTracking().ToListAsync();
			return model;
		}

		public async Task<List<ProtectionOfficeMember>> GetByProtectionOfficeId( int? id )
		{
			var model = await DbContext.Set<ProtectionOfficeMember>()
				.Where( w => w.ProtectionOfficeId == id )
				.Include( c => c.ApplicationUser )
				.AsNoTracking().ToListAsync();
			return model;
		}
		public async Task<List<ProtectionOfficeMember>> GetByProtectionOfficeUserId( List<int> userIdList )
		{
			List<ProtectionOfficeMember> protectionOfficeMemberList = new();
			foreach ( var item in userIdList )
			{
				var model = await DbContext.Set<ProtectionOfficeMember>().Where( w => w.ApplicationUserId == item ).AsNoTracking().ToListAsync();
				protectionOfficeMemberList.AddRange( model.Distinct() );
			}
			return protectionOfficeMemberList;
		}
		public async Task<ProtectionOfficeMember> GetByIdentityId( int id )
		{
			var model =
				await DbContext.Set<ProtectionOfficeMember>().FindAsync( id );
			return model;
		}
		public async Task<IList<ProtectionOfficeMember>> GetListProtectionOfficeMember()
		{
			var result =
				await DbContext.Set<ProtectionOfficeMember>()
				.Include( c => c.ProtectionOffice ).ToListAsync();

			return result;
		}
	}
}