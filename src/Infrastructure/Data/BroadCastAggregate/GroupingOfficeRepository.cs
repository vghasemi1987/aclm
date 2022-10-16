using DomainContracts.BroadcastAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BroadcastAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class GroupingOfficeRepository : EfRepository<GroupingOffice>, IGroupingOfficeRepository
	{
		private readonly DbSet<GroupingOffice> _dbSet;
		public GroupingOfficeRepository( ServerAccessibilityMonitorContext dbContext ) : base( dbContext )
		{
			_dbSet = dbContext.Set<GroupingOffice>();
		}

		public async Task<GroupingOffice> GetById( int? id )
		{
			return await DbContext.Set<GroupingOffice>()
				.Include( c => c.GroupingOfficeMembers )
				.AsNoTracking()
				.SingleOrDefaultAsync( c => c.Id == id );
		}

		public async Task<List<GroupingOffice>> GetGroupingOfficeAll()
		{

			var result = await DbContext.Set<GroupingOffice>()
			.Include( c => c.GroupingOfficeMembers ).AsNoTracking().ToListAsync();

			return result;
		}
		public async Task<List<GroupingOfficeMember>> GetByGroupOfficeId( int? id )
		{
			var model = await DbContext.Set<GroupingOfficeMember>().Where( w => w.GroupingOfficeId == id ).AsNoTracking().ToListAsync();
			return model;
		}
		public async Task<List<ApplicationUser>> GetUserBySubject( List<ApplicationUser> destinationUser , string title )
		{
			//پیدا کردن موضوع مقصد
			//اداره حفاظت فناوری اطلاعات و اسناد
			//73
			GroupingOffice getUsersByTitle =
				await DbContext.Set<GroupingOffice>().Where( w => w.Title.Contains( title ) ).FirstOrDefaultAsync();

			if ( getUsersByTitle != null )
			{
				//		  userId
				//	115 73  1   0
				//	116 73  8   0
				//	117 73  9   0
				List<GroupingOfficeMember> usersId =
					await DbContext.Set<GroupingOfficeMember>()
					.Where( w => w.GroupingOfficeId == getUsersByTitle.Id )
					.ToListAsync();

				List<ApplicationUser> finalResultUsers = new List<ApplicationUser>();

				foreach ( var item in destinationUser )
				{
					foreach ( var user in usersId )
					{
						if ( item.Id == user.ApplicationUserId )
						{
							finalResultUsers.Add( item );
						}
					}
				}

				return finalResultUsers;
			}
			return new List<ApplicationUser>();



			//var result =
			//	await DbContext.Set<GroupingOffice>().Where( w => w.Title.Contains( title ) )
			//	.AsNoTracking().ToListAsync();
			//return result;
		}
	}
	public class GroupingOfficeMemberRepository : EfRepository<GroupingOfficeMember>, IGroupingOfficeMemberRepository
	{
		private readonly DbSet<GroupingOfficeMember> _dbSet;
		public GroupingOfficeMemberRepository( ServerAccessibilityMonitorContext dbContext ) : base( dbContext )
		{
			_dbSet = dbContext.Set<GroupingOfficeMember>();
		}

		public async Task DeleteByGroupingOfficeMemberListAsync( int id )
		{
			List<GroupingOfficeMember> result =
				await ( _dbSet.Where( w => w.GroupingOfficeId == id ) ).ToListAsync();
			foreach ( GroupingOfficeMember item in result )
			{
				_dbSet.Remove( _dbSet.Find( item.Id ) );
			}
		}

		public async Task<List<GroupingOfficeMember>> GetByGroupingOfficeId( int? id )
		{
			var model =
				await DbContext.Set<GroupingOfficeMember>()
				.Where( w => w.GroupingOfficeId == id )
				.Include( c => c.ApplicationUser ).AsNoTracking().ToListAsync();
			return model;
		}
		public async Task<GroupingOfficeMember> GetByUserIdAndGroupingOfficeId( int userId , int groupId )
		{
			var model =
				await DbContext.Set<GroupingOfficeMember>()
				.Where( w => w.ApplicationUserId == userId && w.GroupingOfficeId == groupId )
				.Include( c => c.ApplicationUser ).AsNoTracking()
				.FirstOrDefaultAsync();
			return model;
		}
		public async Task<GroupingOfficeMember> GetByIdentityId( int id )
		{
			var model =
				await DbContext.Set<GroupingOfficeMember>().FindAsync( id );
			return model;
		}
	}
}