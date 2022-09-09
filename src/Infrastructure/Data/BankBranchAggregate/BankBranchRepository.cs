using DomainContracts.BankBranchAggregate;
using DomainEntities.BankBranchAggregate;
using DomainEntities.Commons;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Data.BankBranchAggregate
{
	public class BankBranchRepository : EfRepository<BankBranch>, IBankBranchRepository
	{
		private readonly DbSet<BankBranch> _dbSet;

		public BankBranchRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<BankBranch>();
		}

		public async Task<BankBranch> GetBranchById(int id)
		{
			var result =
				await _dbSet.FindAsync(id);
			return result;
		}

		public async Task<BankBranch> GetBranchByName(string branchName)
		{

			var result = await _dbSet.Where(w => w.Title.Contains(branchName)).FirstOrDefaultAsync();

			if (result == null)
			{
				var bankBranch = new BankBranch
				{
					Id = 0,
					Title = ""
				};
				return bankBranch;
			}
			return result;


		}

		public Task<List<BankBranchDropDownDto>> GetDropDownBankBranchesList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Where(o => o.BranchHead != null)
				.Include(x => x.BranchHead)
				.OrderBy(x => x.BranchHead.Title)
				.ThenBy(x => x.Title)
				.Select(o => new BankBranchDropDownDto
				{
					Title = o.Title,
					BranchHeadTitle = o.BranchHead.Title,
					Id = o.Id
				})
				.AsNoTracking()
				.ToListAsync();
		}
		public Task<List<BankBranchDropDownDto>> GetDropDownBranchHeadsList()
		{
			return _dbSet.Where(q => q.RecordStatus == Convert.ToBoolean(RecordStatus.NotDeleted))
				.Where(o => o.BranchHead == null)
				.Select(o => new BankBranchDropDownDto
				{
					Id = o.Id,
					Title = o.Title
				})
				.AsNoTracking()
				.ToListAsync();
		}

	}
}