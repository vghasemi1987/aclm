using DomainContracts.BroadcastAggregate;
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
		public GroupingOfficeRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<GroupingOffice>();
		}

		public async Task<GroupingOffice> GetById(int? id)
		{
			return await DbContext.Set<GroupingOffice>().Include(c => c.GroupingOfficeMembers).AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
		}

		public async Task<List<GroupingOffice>> GetGroupingOfficeAll()
		{
			return await DbContext.Set<GroupingOffice>().Include(c => c.GroupingOfficeMembers).AsNoTracking().ToListAsync();
		}
		public async Task<List<GroupingOfficeMember>> GetByGroupOfficeId(int? id)
		{
			var model = await DbContext.Set<GroupingOfficeMember>().Where(w => w.GroupingOfficeId == id).AsNoTracking().ToListAsync();
			return model;
		}
	}
	public class GroupingOfficeMemberRepository : EfRepository<GroupingOfficeMember>, IGroupingOfficeMemberRepository
	{
		private readonly DbSet<GroupingOfficeMember> _dbSet;
		public GroupingOfficeMemberRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<GroupingOfficeMember>();
		}

		public async Task DeleteByGroupingOfficeMemberListAsync(int id)
		{
			List<GroupingOfficeMember> result =
				await (_dbSet.Where(w => w.GroupingOfficeId == id)).ToListAsync();
			foreach (GroupingOfficeMember item in result)
			{
				_dbSet.Remove(_dbSet.Find(item.Id));
			}
		}

		public async Task<List<GroupingOfficeMember>> GetById(int? id)
		{
			var model = await DbContext.Set<GroupingOfficeMember>().Where(w => w.GroupingOfficeId == id)
				.Include(c => c.ApplicationUser).AsNoTracking().ToListAsync();
			return model;
		}
	}
}