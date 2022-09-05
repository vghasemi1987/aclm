﻿using DomainContracts.BroadcastAggregate;
using DomainEntities.BroadcastAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.BroadCastAggregate
{
	public class BroadCastRepository : EfRepository<BroadCast>, IBroadCastRepository
	{
		private readonly DbSet<BroadCast> _dbSet;
		public BroadCastRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<BroadCast>();
		}

		public async Task<BroadCast> GetById(int? id)
		{
			return await DbContext.Set<BroadCast>().Include(c => c.ReferralBroadCasts).Where(w => w.Id == id).FirstOrDefaultAsync();
		}
		async Task<IList<BroadCast>> IBroadCastRepository.ListAllBroadCast()
		{
			var result = await _dbSet.Include(c => c.ReferralBroadCasts)
				.AsNoTracking().ToListAsync();
			return result;
		}
	}
}