﻿using DomainContracts.SettingAggregate;
using DomainEntities.SettingAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Data.SettingAggregate
{
	public class SettingRepository : EfRepository<Setting>, ISettingRepository
	{
		private DbSet<Setting> DbSet { get; }
		public SettingRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			DbSet = dbContext.Set<Setting>();
		}

		public Task<Setting> FindByIdAsync(short id)
		{
			return DbSet.FindAsync(id).AsTask();
		}
	}
}